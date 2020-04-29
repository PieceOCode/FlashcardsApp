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
            
            TurnButtonCommand = new Command(execute: turnPage);
            SwitchLeftButtonCommand = new Command(execute: switchLeft);
            SwitchRightButtonCommand = new Command(execute: switchRight);
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("Property Changed");
            if(e.PropertyName.Equals(nameof(Model.FrontText)))
            {
                Console.WriteLine("New Front text");
                OnPropertyChanged(nameof(FrontText));
            } else if (e.PropertyName.Equals(nameof(Model.BackText)))
            {
                OnPropertyChanged(nameof(BackText));
            }
        }

        void turnPage()
        {
            Console.WriteLine("Card turned");
            IsTurned = !IsTurned;
        }

        void switchLeft()
        {
            Model.SwitchCard(-1);
        }
        void switchRight()
        {
            Model.SwitchCard(1);
        }
    }
}
