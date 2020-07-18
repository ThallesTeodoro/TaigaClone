using Taiga.Core.Interfaces.ServicesInterfaces;

namespace Taiga.Core.Services
{
    public class UnitServiceOfWork : IUnitServiceOfWork
    {
        private IConfirmationCodeNotificationService _configurationCode;
        private ITowFactorNotificationService _towFactorAuthentication;
        private IJwtService _jwtService;
        private ILoginNotificationService _loginNotification;

        public IConfirmationCodeNotificationService ConfirmationCodeNotificationService
        {
            get { return _configurationCode = _configurationCode ?? new ConfirmationCodeNotificationService(); }
        }

        public ITowFactorNotificationService TowFactorNotificationService
        {
            get { return _towFactorAuthentication = _towFactorAuthentication ?? new TowFactorNotificationService(); }
        }

        public IJwtService JwtService
        {
            get { return _jwtService = _jwtService ?? new JwtService(); }
        }

        public ILoginNotificationService LoginNotificationService
        {
            get { return _loginNotification = _loginNotification ?? new LoginNotificationService(); }
        }
    }
}
