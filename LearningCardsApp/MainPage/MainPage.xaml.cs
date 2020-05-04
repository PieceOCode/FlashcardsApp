using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LearningCardsApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private CardModel theModel;
        public MainPage()
        {
            InitializeComponent();

            RandomButton.Clicked += RandomButtonClicked;
            ViewAllButton.Clicked += ViewAllButtonClicked;

            theModel = new CardModel();
        }

        private async void  RandomButtonClicked(object sender, EventArgs e)
        {
            CardPageViewModel vm = new CardPageViewModel(theModel);
            var nextPage = new CardPage(vm);
            await Navigation.PushAsync(nextPage, true);
        }

        private async void ViewAllButtonClicked(object sender, EventArgs e)
        {
            SubjectPageViewModel vm = new SubjectPageViewModel(theModel);
            var nextPage = new SubjectPage(vm);
            await Navigation.PushAsync(nextPage, true);
        }
    }
}
