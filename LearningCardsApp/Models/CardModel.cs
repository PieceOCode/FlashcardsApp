using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCardsApp
{
    class CardModel : ModelBase
    {
        private string frontText;
        private string backText;
        private bool isTurned = false;

        public string FrontText
        {
            get => frontText;
            set
            {
                if (value != frontText)
                {
                    frontText = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BackText
        {
            get => backText;
            set
            {
                if (value != backText)
                {
                    backText = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public bool IsNotTurned
        {
            get => !isTurned;
        }

        public CardModel()
        {
            FrontText = "EmptyFront";
            BackText = "EmptyBack";
        }

        public CardModel(string front, string back)
        {
            FrontText = front;
            BackText = back;
        }
    }
}
