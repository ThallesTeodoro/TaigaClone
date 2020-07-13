using Taiga.Core.Interfaces.ServicesInterfaces;

namespace Taiga.Core.Services
{
    public class UnitServiceOfWork : IUnitServiceOfWork
    {
        private IConfirmationCodeNotificationService _configurationCode;
        private IJwtService _jwtService;

        public IConfirmationCodeNotificationService ConfirmationCodeNotificationService
        {
            get { return _configurationCode = _configurationCode ?? new ConfirmationCodeNotificationService(); }
        }

        public IJwtService JwtService
        {
            get { return _jwtService = _jwtService ?? new JwtService(); }
        }
    }
}
