using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCardsApp
{
    class CardModel : ModelBase
    {
        private List<Card> cards;
        private int cardIndex;
        private bool isTurned = false;

        public string FrontText
        {
            get => cards[cardIndex].frontText;
            set
            {
                if (value != cards[cardIndex].frontText)
                {
                    cards[cardIndex] = new Card(value, cards[cardIndex].backText);
                    OnPropertyChanged();
                }
            }
        }

        public string BackText
        {
            get => cards[cardIndex].backText;
            set
            {
                if (value != cards[cardIndex].backText)
                {
                    cards[cardIndex] = new Card(cards[cardIndex].frontText, value);
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
            cards = new List<Card>();
            cardIndex = 0;
            cards.Add(new Card("Text Front", "TextBack"));
        }

        public void AddCard (string frontText, string backText)
        {
            Card card = new Card(frontText, backText);
            cards.Add(card);
        }

        public void SwitchCard(int steps = 1)
        {
            cardIndex += steps;
            cardIndex %= cards.Count;
        } 
    }

    struct Card
    {
        public string frontText;
        public string backText;

        public Card(string front = "Empty Front", string back = "Empty Back") 
        {
            frontText = front;
            backText = back;
        }
    }
}