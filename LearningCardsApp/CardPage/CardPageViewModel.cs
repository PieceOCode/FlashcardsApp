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
        public ICommand CategoryButtonCommand { get; set; }


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
            CategoryButtonCommand = new Command(execute: changeCategory);
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

        void changeCategory()
        {
            Model.ChangeCategory(Model.GetCategories()[1]);
            IsTurned = false;
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
    }
}
