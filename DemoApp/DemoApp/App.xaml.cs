using System;
using DemoApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new string[] { "Shapes_Experimental" });
            MainPage = new NavigationPage(Resolver.Resolve<DemoApp.Views.MainPage>());
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
    }
}
