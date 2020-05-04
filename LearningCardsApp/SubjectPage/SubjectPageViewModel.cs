using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace LearningCardsApp
{
    public class SubjectPageViewModel : ViewModelBase<CardModel>
    {
        private List<string> _subjects;
        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
        public List<string> Subjects
        {
            get => _subjects;
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

            List<string> categories = Model.GetCategories();
        }
    }
}
