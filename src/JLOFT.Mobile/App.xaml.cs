using System;
using JLOFT.Mobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JLOFT.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
#if DEBUG
            Preferences.Set(AppSettings._preferences_IsDeviceActivated, true);
#endif

            MainPage = new NavigationPage(DetermineMainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private Page DetermineMainPage()
        {
            if (Preferences.Get(AppSettings._preferences_IsDeviceActivated, false))
            {
                return new LoginView();
            }
            else
            {
                return new ActivateDeviceView();
            }
        }
    }
}

