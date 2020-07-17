using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Taiga.Core.Entities;
using Taiga.Api.Extensions;
using Taiga.Core.Interfaces;
using Taiga.Core.Interfaces.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Taiga.Core.Services;
using Taiga.Api.Utilities;

namespace Taiga.Api.Features.Login
{
    [ApiController]
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly IUnitServiceOfWork _usow;

        public LoginController(
            ILogger<LoginController> logger,
            IUnitOfWork uow,
            IConfiguration configuration,
            IUnitServiceOfWork usow)
        {
            _logger = logger;
            _uow = uow;
            _configuration = configuration;
            _usow = usow;
        }

        [Route("")]
        [HttpPost]
        public IActionResult Auth([FromBody] AuthViewModel model)
        {
            JsonResponse response = new JsonResponse(200);

            try
            {
                if (!ModelState.IsValid)
                {
                    response.StatusCode = 422;
                    response.Message = "Unprocessable Entity.";
                    response.Errors = Validations.FormatViewModelErrors(ModelState);
                }
                else
                {
                    User user = _uow.UserRepository.FindByEmail(model.Email);

                    if (user == null || HashExtension.Validate(
                        model.Password,
                        Environment.GetEnvironmentVariable("AUTH_SALT"),
                        user.Password) == false)
                    {
                        ModelState.AddModelError("Email", "Invalid Credentials.");
                        response.StatusCode = 422;
                        response.Message = "Unprocessable Entity.";
                        response.Errors = Validations.FormatViewModelErrors(ModelState);
                    }
                    else
                    {
                        if (user.EmailConfirmed != true)
                        {
                            ModelState.AddModelError("Email", "Unverified Email.");
                            response.StatusCode = 401;
                            response.Message = "Unauthorized.";
                            response.Errors = Validations.FormatViewModelErrors(ModelState);
                        }
                        else
                        {
                            SendEmailNotification(user);
                            _uow.Commit();
                            response.Message = "Ok";
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("Login error: " + exception);
                response.StatusCode = 500;
                response.Message = "Internal Server Error.";
                _uow.Rollback();
            }

            HttpContext.Response.StatusCode = response.StatusCode;
            return Json(response);
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult Confirm([FromBody]ConfirmCodeViewModel model)
        {
            JsonResponse response = new JsonResponse(200);

            try
            {
                if (ModelState.IsValid)
                {
                    EmailConfirmationCode emailConfirmation = _uow.EmailConfirmationCodeRepository.FindUniqueByEmail(model.Email,
                                                                                                                     CodeType.Login);

                    if (emailConfirmation == null)
                    {
                        ModelState.AddModelError("Email", "Email not found. Please, make sure you are registered.");
                        response.StatusCode = 404;
                        response.Message = "Email Not Found.";
                        response.Errors = Validations.FormatViewModelErrors(ModelState);
                    }
                    else
                    {
                        User user = _uow.UserRepository.FindByEmail(model.Email);

                        if (user != null)
                        {
                            ConfirmationCodeValidation confirmationCode = new ConfirmationCodeValidation(_uow,
                                                                                                         _configuration);

                            switch (confirmationCode.ValidateConfirmationCode(model.Email,
                                                                              model.Code,
                                                                              emailConfirmation.Code))
                            {
                                case 200:
                                    user.EmailConfirmed = true;
                                    _uow.UserRepository.Update(user);
                                    _uow.EmailConfirmationCodeRepository.Remove(emailConfirmation.Id);
                                    string secret = _configuration.GetSection("JwtConfig").GetSection("Secret").Value;
                                    string expDate = _configuration.GetSection("JwtConfig").GetSection("ExpirationTime").Value;
                                    var token = _usow.JwtService.GenerateToken(user, secret, expDate);
                                    response.StatusCode = 200;
                                    response.Message = "Successfully Registered.";
                                    response.Message = "Ok";
                                    response.Data = new
                                    {
                                        Id = user.Id,
                                        Email = user.Email,
                                        UserName = user.UserName,
                                        Token = token
                                    };
                                    break;
                                case 401:
                                    response.StatusCode = 401;
                                    response.Message = "Invalid Confirmation Code.";
                                    break;
                                case 410:
                                    response.StatusCode = 410;
                                    response.Message = "Expired Confirmation Code.";
                                    break;
                                case 429:
                                    response.StatusCode = 429;
                                    response.Message = "Attempt Limit.";
                                    break;
                                default:
                                    response.StatusCode = 401;
                                    response.Message = "Invalid Confirmation Code.";
                                    break;
                            }

                            _uow.Commit();
                        }
                        else
                        {
                            response.StatusCode = 404;
                            response.Message = "User not found. Please, try to register again.";
                        }
                    }
                }
                else
                {
                    response.StatusCode = 422;
                    response.Message = "Unprocessable Entity.";
                    response.Errors = Validations.FormatViewModelErrors(ModelState);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("Login Confirm Error: ", exception);
                response.StatusCode = 500;
                response.Message = "Internal Server Error.";
            }

            HttpContext.Response.StatusCode = response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Send confirmation email notification
        /// </summary>
        /// <param name="user"></param>
        private void SendEmailNotification(User user)
        {
            EmailConfirmationCode emailCode = _uow.EmailConfirmationCodeRepository.FindUniqueByEmail(user.Email, CodeType.Login);

            if (emailCode == null)
            {
                Random random = new Random();
                emailCode = new EmailConfirmationCode
                {
                    Email = user.Email,
                    Code = random.Next(10000, 99999),
                    Type = CodeType.Login,
                    CreatedAt = DateTime.Now
                };
                _uow.EmailConfirmationCodeRepository.Add(emailCode);
            }

            _usow.TowFactorNotificationService.SendNotification(user.UserName, user.Email, emailCode.Code);
        }
    }
}
