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
        public MainPage()
        {
            InitializeComponent();

            RandomButton.Clicked += RandomButtonClicked;
            ViewAllButton.Clicked += ViewAllButtonClicked;
        }

        private async void  RandomButtonClicked(object sender, EventArgs e)
        {
            var nextPage = new CardPage();
            nextPage.BindingContext = new CardModel();
            await Navigation.PushAsync(nextPage, true);
        }

        private async void ViewAllButtonClicked(object sender, EventArgs e)
        {
            return;
        }
    }
}
