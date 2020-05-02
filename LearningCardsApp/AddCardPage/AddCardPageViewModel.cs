using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;

namespace LearningCardsApp
{
    public class AddCardPageViewModel : ViewModelBase<CardModel>
    {
        private string category;
        
        public ICommand AddCardCommand { get; set; }

        public string FrontText { get; set; }
        public string BackText { get; set; }
        public string Category
        {
            get => category;
            set
            {
                if(value != category) {
                    category = value;
                    OnPropertyChanged(Category);
                }
            }
        }
        public List<string> Categories { get; set; }

        public AddCardPageViewModel(CardModel m = null)
        {
            Model = m;
            
            Categories = Model.GetCategories();
            OnPropertyChanged("Categories");
            Category = Categories[0];
            OnPropertyChanged("Category");


            AddCardCommand = new Command(execute: addCard);
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private void addCard()
        {
            Model.AddCard(FrontText, BackText, Category);
            Navigation.PopAsync();
        }
    }
}
