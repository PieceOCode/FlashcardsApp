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
        public SubjectPage(SubjectPageViewModel model = null)
        {
            InitializeComponent();

            BindingContext = model ?? new SubjectPageViewModel();
        }

        //INavigation IPage.NavigationProxy => Navigation;

    }
}

