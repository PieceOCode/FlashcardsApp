using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCardsApp
{
    public class CardModel : ModelBase
    {
        private List<Card> cards;
        private Dictionary<string, List<Card>> CardsByCategory;
        private string currentCategory;

        private int cardIndex;
       
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

        public string CurrentCategory
        {
            get => currentCategory;
            set
            {
                if(value != currentCategory)
                {
                    currentCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public CardModel()
        {
            CardsByCategory = new Dictionary<string, List<Card>>();

            List<Card> fruits = new List<Card>();
            fruits.Add(new Card("Apple", "Red"));
            fruits.Add(new Card("Strawberry", "Red"));
            fruits.Add(new Card("Banana", "Yellow"));
            fruits.Add(new Card("Cucumber", "Green"));

            CardsByCategory.Add("Fruit's Colors", fruits);


            List<Card> spirits = new List<Card>();
            spirits.Add(new Card("Bacardi", "Rum"));
            spirits.Add(new Card("Havana Club", "Rum"));
            spirits.Add(new Card("Crown Royal", "Whisky"));
            spirits.Add(new Card("Absolut", "Vodka"));

            CardsByCategory.Add("Spirit Brands", spirits);

            ChangeCategory("Fruit's Colors");
        }

        public void AddCard (string frontText, string backText, string category)
        {
            Card card = new Card(frontText, backText);
            if(!CardsByCategory.ContainsKey(category)) {
                AddCategory(category);
            }
            CardsByCategory[category].Add(card);
            ChangeCategory(category);
        }
        public void DeleteCard()
        {
            CardsByCategory[currentCategory].RemoveAt(cardIndex);
            cards = CardsByCategory[currentCategory];
            SwitchCard(1);
        }

        public void DeleteCategory()
        {
            CardsByCategory.Remove(CurrentCategory);
            string[] categoryCards = new string[CardsByCategory.Keys.Count];
            CardsByCategory.Keys.CopyTo(categoryCards, 0);
            ChangeCategory(categoryCards[0]);
        }

        public void SwitchCard(int steps = 1)
        {
            Console.WriteLine("Switching");
            cardIndex += steps;
            steps %= cards.Count;
            if (cardIndex < 0) cardIndex += cards.Count;
            cardIndex %= cards.Count;
            OnPropertyChanged("FrontText");
            OnPropertyChanged("BackText");
        } 

        public List<string> GetCategories()
        {
            string[] categoryCards = new string[CardsByCategory.Keys.Count];
            CardsByCategory.Keys.CopyTo(categoryCards, 0);
            return new List<string>(categoryCards);
        }

        public void AddCategory(string cat)
        {
            CardsByCategory.Add(cat, new List<Card>());
        }

        public void ChangeCategory (string category)
        {
            cards = CardsByCategory[category];
            CurrentCategory = category;
            cardIndex = 0;

            OnPropertyChanged("FrontText");
            OnPropertyChanged("BackText");
        }

    }

    public struct Card
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