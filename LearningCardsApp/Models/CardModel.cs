using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCardsApp
{
    public class CardModel : ModelBase
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
            cards.Add(new Card("Apple", "Red"));
            cards.Add(new Card("Strawberry", "Red"));
            cards.Add(new Card("Banana", "Yellow"));

            cards.Add(new Card("Cucumber", "Green"));
        }

        public void AddCard (string frontText, string backText)
        {
            Card card = new Card(frontText, backText);
            cards.Add(card);
        }

        public void SwitchCard(int steps = 1)
        {
            Console.WriteLine("Switching");
            cardIndex += steps;
            cardIndex %= cards.Count;
            OnPropertyChanged("FrontText");
            OnPropertyChanged("BackText");
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