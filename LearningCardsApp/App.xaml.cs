using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningCardsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage main = new MainPage();
            MainPage = new NavigationPage(main);
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
