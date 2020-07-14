using System;
using Microsoft.Extensions.Configuration;
using Taiga.Core.Entities;
using Taiga.Core.Interfaces;

namespace Taiga.Api.Utilities
{
    public class ConfirmationCodeValidation
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public ConfirmationCodeValidation(
            IUnitOfWork uow,
            IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        /// <summary>
        /// Validate the confirmation code
        /// </summary>
        /// <param name="model"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int ValidateConfirmationCode(string modelEmail, int modelCode, int code)
        {
            int attempts = CheckAttempts(modelEmail);
            bool confirmed = false;
            if (modelCode == code)
            {
                confirmed = true;
            }

            if (confirmed && (attempts == 410 || attempts == 429))
            {
                return attempts;
            }
            else if (!confirmed && (attempts == 410 || attempts == 429))
            {
                return attempts;
            }
            else if (!confirmed && attempts == 200)
            {
                return 401;
            }

            return 200;
        }

        /// <summary>
        /// Check the confirmation email attempts quantity
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private int CheckAttempts(string email)
        {
            AttemptsQuantity attempts = _uow.AttemptsQuantityRepository.FindUniqueByEmailAndType(email, EndpointType.ConfirmEmail);

            if (attempts == null)
            {
                attempts = new AttemptsQuantity
                {
                    Email = email,
                    Quantity = 1,
                    Type = EndpointType.ConfirmEmail,
                    CreatedAt = DateTime.Now
                };

                _uow.AttemptsQuantityRepository.Add(attempts);
                return 200;
            }

            int attemptsLimit = Int32.Parse(_configuration.GetSection("AttemptsLimit").Value.ToString());

            TimeSpan diff = DateTime.Now - attempts.CreatedAt;
            double diffHours = diff.TotalHours;

            if (diffHours > 1)
            {
                _uow.AttemptsQuantityRepository.Remove(attempts.Id);
                return 410;
            }

            if (attemptsLimit <= attempts.Quantity)
            {
                _uow.AttemptsQuantityRepository.Remove(attempts.Id);
                return 429;
            }

            attempts.Quantity += 1;
            _uow.AttemptsQuantityRepository.Update(attempts);

            return 200;
        }
    }
}
