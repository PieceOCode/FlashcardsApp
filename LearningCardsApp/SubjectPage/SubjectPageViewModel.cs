using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCardsApp
{
    public class SubjectPageViewModel : ViewModelBase
    {
        //**********************  PRIVATE MEMBER VARIABLES *********************
        private List<string> _subjects;

        // ***********************  BINDABLE PROPERTIES ************************

        // ItemSource binds to an IEnumerable
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

        // ***************************  CONSTRUCTOR ****************************
        public SubjectPageViewModel(SubjectPage m = null) 
        {
            Subjects = new List<string>
            {
                "Mercury",
                "Venus",
                "Jupiter",
                "Earth",
                "Mars",
                "Saturn",
                "Pluto"
            };
        }
    }
}
