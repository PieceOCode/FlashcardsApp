using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;
using System.Diagnostics;

namespace LearningCardsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string path = Path.Combine(FileSystem.AppDataDirectory, "cards.xml");
            CardModel m = CardModel.Read(path);
            if(m==null)
            {
                m = new CardModel();
            }
            m.Save(path);

            MainPage main = new MainPage(m);
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
