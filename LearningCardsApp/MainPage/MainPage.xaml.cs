using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LearningCardsApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private CardModel theModel;
        public MainPage(CardModel m = null)
        {
            InitializeComponent();

            RandomButton.Clicked += RandomButtonClicked;
            ViewAllButton.Clicked += ViewAllButtonClicked;

            theModel = m;
        }

        private async void  RandomButtonClicked(object sender, EventArgs e)
        {
            List<string> cards = theModel.GetCategories();
            if (cards.Count > 0)
            {
                Random rnd = new Random();
                int r = rnd.Next(cards.Count);
                theModel.ChangeCategory(cards[r]);
                CardPageViewModel vm = new CardPageViewModel(theModel);
                var nextPage = new CardPage(vm);
                await Navigation.PushAsync(nextPage, true);
            } else
            {
                await DisplayAlert("No Categories found.", "There are no existing categories or cards, please create some first by going to ALL CARDS", "OK");
            }
        }

        private async void ViewAllButtonClicked(object sender, EventArgs e)
        {
            SubjectPageViewModel vm = new SubjectPageViewModel(theModel);
            var nextPage = new SubjectPage(vm);
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
