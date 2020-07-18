using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Taiga.Api.Extensions;
using Taiga.Api.Utilities;
using Taiga.Api.Features.Register.ViewModels;
using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Core.Interfaces.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Taiga.Api.Filters;

namespace Taiga.Api.Features.Register
{
    [ApiController]
    [Route("register")]
    [AnonymousOnlyFilter("Index", "Dashboard")]
    public class RegisterController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _environment;
        private readonly IUnitServiceOfWork _usow;
        private readonly IConfiguration _configuration;

        public RegisterController(
            ILogger<RegisterController> logger,
            IUnitOfWork uow,
            IWebHostEnvironment environment,
            IUnitServiceOfWork usow,
            IConfiguration configuration)
        {
            _logger = logger;
            _uow = uow;
            _environment = environment;
            _usow = usow;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                if (ModelState.IsValid)
                {
                    User user = _uow.UserRepository.FindByEmail(model.Email);

                    if (user != null)
                    {
                        ModelState.AddModelError("Email", "Email Already Registered.");
                        response.StatusCode = 422;
                        response.Message = "Unprocessable Entity.";
                        response.Errors = Validations.FormatViewModelErrors(ModelState);
                    }
                    else
                    {
                        int count = _uow.UserRepository.CountByUserName(model.UserName);
                        string userName = model.UserName;
                        if (count > 0)
                        {
                            userName += count + 1;
                        }

                        user = new User
                        {
                            Name = model.Name,
                            UserName = userName,
                            Email = model.Email,
                            Password = HashExtension.Create(
                                model.Password,
                                Environment.GetEnvironmentVariable("AUTH_SALT")),
                            Biography = model.Biography,
                            CreatedAt = DateTime.Now,
                            EmailConfirmed = false,
                        };

                        if (model.Avatar != null)
                        {
                            user.Avatar = FileManagement.SaveFile(
                                model.Avatar,
                                Path.Combine(_environment.WebRootPath, "img/users")).ToString();
                        }

                        _uow.UserRepository.Add(user);
                        SendEmailNotification(user);
                        _uow.Commit();

                        response.StatusCode = 200;
                        response.Message = "Successfully Registred.";
                        response.Data = user;
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
                _logger.LogError("Register user error: " + exception);
                _uow.Rollback();
                response.StatusCode = 500;
                response.Message = "Internal Server Error.";
            }

            HttpContext.Response.StatusCode = response.StatusCode;
            return Json(response);
        }

        [HttpPost]
        [Route("confirm")]
        public IActionResult Confirm([FromBody] ConfirmCodeViewModel model)
        {
            JsonResponse response = new JsonResponse();

            try
            {
                if (ModelState.IsValid)
                {
                    EmailConfirmationCode emailConfirmation = _uow.EmailConfirmationCodeRepository.FindUniqueByEmail(model.Email,
                                                                                                                     CodeType.Register);

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
                                    response.StatusCode = 200;
                                    response.Message = "Successfully Registred.";
                                    user.EmailConfirmed = true;
                                    _uow.UserRepository.Update(user);
                                    _uow.EmailConfirmationCodeRepository.Remove(emailConfirmation.Id);
                                    break;
                                case 401:
                                    response.StatusCode = 401;
                                    response.Message = "Invalid Confirmation Code.";
                                    break;
                                case 410:
                                    response.StatusCode = 410;
                                    response.Message = "Expired Confirmation Code.";
                                    _uow.UserRepository.Remove(_uow.UserRepository.FindByEmail(model.Email).Id);
                                    break;
                                case 429:
                                    response.StatusCode = 429;
                                    response.Message = "Attempt Limit.";
                                    _uow.UserRepository.Remove(_uow.UserRepository.FindByEmail(model.Email).Id);
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
                _logger.LogError("Register Confirm error: " + exception);
                _uow.Rollback();
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
            EmailConfirmationCode emailCode = _uow.EmailConfirmationCodeRepository.FindUniqueByEmail(user.Email, CodeType.Register);

            if (emailCode == null)
            {
                Random random = new Random();
                emailCode = new EmailConfirmationCode
                {
                    Email = user.Email,
                    Code = random.Next(10000, 99999),
                    Type = CodeType.Register,
                    CreatedAt = DateTime.Now
                };
                _uow.EmailConfirmationCodeRepository.Add(emailCode);
            }

            _usow.ConfirmationCodeNotificationService.SendNotification(user.UserName, user.Email, emailCode.Code);
        }
    }
}
