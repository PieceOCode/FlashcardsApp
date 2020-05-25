using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace LearningCardsApp
{
    public class SubjectPageViewModel : ViewModelBase<CardModel>
    {
        public ICommand AddCategoryButtonCommand { get; set; }


        private List<string> _subjects;
        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
        public List<string> Subjects
        {
            get => Model.GetCategories();
            set
            {
                if (_subjects == value) return;
                _subjects = value;
                OnPropertyChanged();
            }
        }

        public SubjectPageViewModel(CardModel m = null) 
        {
            Model = m ?? new CardModel();

            Subjects = Model.GetCategories();

            AddCategoryButtonCommand = new Command(execute: addCategory);
        }

        public void NavigateCardPage(int index)
        {
            Model.ChangeCategory(Subjects[index]);
            CardPageViewModel vm = new CardPageViewModel(Model);
            CardPage page = new CardPage(vm);
            Navigation.PushAsync(page);
        }
        public async void addCategory()
        {
            string result = await App.Current.MainPage.DisplayPromptAsync("Category Name:", "");
            Model.AddCategory(result);
            OnPropertyChanged("Subjects");
        }

    }
}
