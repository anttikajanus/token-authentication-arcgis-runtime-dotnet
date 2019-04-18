using Esri.ArcGISRuntime.Http;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;
using Prism.Commands;
using Prism.Events;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using TokenAuthentication.Example.Core;
using TokenAuthentication.Example.Events;

namespace TokenAuthentication.Example.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private const string PortalUrl = "https://www.arcgis.com/sharing/rest";
      
        public LoginViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            LoginCommand = new DelegateCommand<IHavePassword>(Login);
            UserName = "akajanus";
            //TODO : Get credentials from Windows Credential Vault, if they have been saved
            EventAggregator.GetEvent<UserSessionEndedEvent>().Subscribe(args =>
            {
                if (args.EndingType == UserSessionEndedMessage.SessionEndingType.SwitchUser)
                    UserName = string.Empty;
                AuthenticationManager.Current.RemoveAllCredentials();
            });
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private bool? _rememberMe;
        public bool? RememberMe
        {
            get { return _rememberMe; }
            set { SetProperty(ref _rememberMe, value); }
        }

        private bool _isSigningIn;
        public bool IsSigningIn
        {
            get { return _isSigningIn; }
            set { SetProperty(ref _isSigningIn, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public DelegateCommand<IHavePassword> LoginCommand { get; }

        private async void Login(IHavePassword parameter)
        {
            try
            {
                if (IsSigningIn) return;
                IsSigningIn = true;
                ErrorMessage = string.Empty;

                var userCredentials = await AuthenticationManager.Current.GenerateCredentialAsync(
                    new Uri(PortalUrl),
                    UserName, 
                    ConvertToUnsecureString(parameter.Password));

                //TODO: if remember me is checked, save creadentials to the Windows Credential Vault

                // Make sure that the credentials are saved for later use
                if (!AuthenticationManager.Current.Credentials.Contains(userCredentials))
                    AuthenticationManager.Current.AddCredential(userCredentials);

                var portal = await ArcGISPortal.CreateAsync(new Uri(PortalUrl), userCredentials, CancellationToken.None);
                EventAggregator.GetEvent<UserSessionCreatedEvent>().Publish(
                    new UserSessionCreatedMessage(portal));
            }
            catch (ArcGISWebException ex)
            {
                ErrorMessage = ex.Details.First();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Authentication failed due unexpected error.";
            }
            finally
            {
                IsSigningIn = false;
                parameter.ClearPassword();
            }
        }

        private string ConvertToUnsecureString(SecureString value)
        {
            if (value == null)
                return string.Empty;

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
