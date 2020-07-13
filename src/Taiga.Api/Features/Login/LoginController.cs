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
                    User user = _uow.UserRepository.FindUniqueByEmailOrUserName(model.UserName);

                    if (user == null || HashExtension.Validate(
                        model.Password,
                        Environment.GetEnvironmentVariable("AUTH_SALT"),
                        user.Password) == false)
                    {
                        response.StatusCode = 422;
                        response.Message = "Uprocessable Entity.";
                        response.Errors = new Dictionary<string, string>
                        {
                            {"Email", "Invalid Credentials."}
                        };
                    }
                    else
                    {
                        string secret = _configuration.GetSection("JwtConfig").GetSection("Secret").Value;
                        string expDate = _configuration.GetSection("JwtConfig").GetSection("ExpirationTime").Value;
                        var token = _usow.JwtService.GenerateToken(user, secret, expDate);

                        response.Message = "Authenticated";
                        response.Data = new
                        {
                            Id = user.Id,
                            Email = user.Email,
                            UserName = user.UserName,
                            Token = token
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("Login error: " + exception);
                response.StatusCode = 500;
                response.Message = "Internal Server Error.";
            }

            HttpContext.Response.StatusCode = response.StatusCode;
            return Json(response);
        }
    }
}
