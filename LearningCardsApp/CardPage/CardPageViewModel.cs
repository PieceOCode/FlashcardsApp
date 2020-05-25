using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningCardsApp
{
    public class CardPageViewModel : ViewModelBase<CardModel>
    {
        private bool isTurned;

        public ICommand TurnButtonCommand { get; set; }
        public ICommand SwitchLeftButtonCommand { get; set; }
        public ICommand SwitchRightButtonCommand { get; set; }
        public ICommand AddCardButtonCommand { get; set; }
        public ICommand EditCardButtonCommand { get; set; }
        public ICommand DeleteCardButtonCommand { get; set; }
        public ICommand DeleteCategoryButtonCommand { get; set; }
        public ICommand EditCategoryButtonCommand { get; set; }



        public bool IsTurned
        {
            get => isTurned;
            set
            {
                if(value != isTurned)
                {
                    isTurned = value;
                    OnPropertyChanged("IsTurned");
                    OnPropertyChanged("IsNotTurned");
                }
            }
        }

        public string Category
        {
            get => Model.CurrentCategory;
        }

        public bool IsNotTurned => !isTurned;
        public string FrontText {
            get => Model.FrontText;
        }
        public string BackText
        {
            get => Model.BackText;
        }



        public CardPageViewModel(CardModel m = null)
        {
            Model = m ?? new CardModel();
            
            TurnButtonCommand = new Command(execute: turnCard);
            SwitchLeftButtonCommand = new Command(execute: switchLeft);
            SwitchRightButtonCommand = new Command(execute: switchRight);
            AddCardButtonCommand = new Command(execute: NavigateAddCardPage);
            EditCardButtonCommand = new Command(execute: editCard);
            DeleteCardButtonCommand = new Command(execute: deleteCard);
            DeleteCategoryButtonCommand = new Command(execute: deleteCategory);
            EditCategoryButtonCommand = new Command(execute: editCategory);

        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(Model.FrontText)))
            {
                OnPropertyChanged(nameof(FrontText));
            } else if (e.PropertyName.Equals(nameof(Model.BackText)))
            {
                OnPropertyChanged(nameof(BackText));
            } else if (e.PropertyName.Equals(nameof(Model.CurrentCategory)))
            {
                OnPropertyChanged(nameof(Category));
            }
        }

        void NavigateAddCardPage()
        {
            AddCardPageViewModel vm = new AddCardPageViewModel(Model);
            ContentPage page = new AddCardPage(vm);
            Navigation.PushAsync(page, true);
        }

        void turnCard()
        {
            Console.WriteLine("Card turned");
            IsTurned = !IsTurned;
        }

        void switchLeft()
        {
            IsTurned = false;
            Model.SwitchCard(-1);
        }
        void switchRight()
        {
            IsTurned = false;
            Model.SwitchCard(1);
        }

        async void editCard()
        {
            string newFront = await App.Current.MainPage.DisplayPromptAsync("Front Text:", "Enter a new value for the front of the card:", placeholder: FrontText) ;
            string newBack = await App.Current.MainPage.DisplayPromptAsync("Back Text:", "Enter a new value for the back of the card", placeholder: BackText);
            if (newFront != "" && newBack != "")
            {
                Model.ChangeCard(newFront, newBack);
            }
        }

        void deleteCard()
        {
            Model.DeleteCard();
        }
        void deleteCategory()
        {
            Model.DeleteCategory();
        }
        private async void editCategory()
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Category Name:", "");
            Model.EditCategory(result);
            OnPropertyChanged("Subjects");
        }
    }
}
