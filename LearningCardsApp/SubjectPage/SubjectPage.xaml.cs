using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningCardsApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectPage : ContentPage 
    {
        private SubjectPageViewModel vm;
        public SubjectPage(SubjectPageViewModel model = null)
        {
            InitializeComponent();

            vm = model;
            BindingContext = model ?? new SubjectPageViewModel();
            SubjetsListView.ItemTapped += CategoriesListView_Tapped;
        }
        private void CategoriesListView_Tapped(object sender, ItemTappedEventArgs e)
        {
            Console.WriteLine("Lalala");

            int index = e.ItemIndex;
            vm.NavigateCardPage(index);
        }
    }
}

