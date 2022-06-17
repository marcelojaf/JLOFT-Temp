using System;
namespace JLOFT.Mobile.ViewModels
{
	public class LoginViewModel : BaseLoginViewModel
	{
		public LoginViewModel()
		{
            ShowNavigationBackButton = false;
            ShowNavigationBar = false;
            DisableBackButtonPressed = true;
        }

        private string _Email = "";
        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }

        private string _Password = "";
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        private bool _RememberMe;
        public bool RememberMe
        {
            get => _RememberMe;
            set => SetProperty(ref _RememberMe, value);
        }

        private bool _HidePassword = true;
        public bool HidePassword
        {
            get => _HidePassword;
            set => SetProperty(ref _HidePassword, value);
        }

        private string _AppVersion = Xamarin.Essentials.VersionTracking.CurrentVersion;
        public string AppVersion
        {
            get => _AppVersion;
            set => SetProperty(ref _AppVersion, value);
        }
    }
}

