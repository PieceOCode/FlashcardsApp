using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using Xamarin.Forms;

using System.IO;

namespace LearningCardsApp
{
    public class CardModel : ModelBase
    {
        //Saves the path the model is saved to
        private string path; 

        private List<Card> cards;
        private Dictionary<string, List<Card>> CardsByCategory;
        private string currentCategory;

        private int cardIndex;


        [XmlIgnore]
        public string FrontText
        {
            get
            {
                if (cardIndex >= cards.Count) return "No cards yet";
                else return cards[cardIndex].frontText;
            }
            set
            {
                if (value != cards[cardIndex].frontText)
                {
                    cards[cardIndex] = new Card(value, cards[cardIndex].backText);
                    OnPropertyChanged();
                }
            }
        }


        [XmlIgnore]
        public string BackText
        {
            get
            {
                if (cardIndex >= cards.Count) return "No cards yet";
                else return cards[cardIndex].backText;
            }
            set
            {
                if (value != cards[cardIndex].backText)
                {
                    cards[cardIndex] = new Card(cards[cardIndex].frontText, value);
                    OnPropertyChanged();
                }
            }
        }

        [XmlIgnore]
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
            CustomSave();
        }

        public void DeleteCategory()
        {
            CardsByCategory.Remove(CurrentCategory);
            string[] categoryCards = new string[CardsByCategory.Keys.Count];
            CardsByCategory.Keys.CopyTo(categoryCards, 0);
            ChangeCategory(categoryCards[0]);
            CustomSave();
        }

        public void DeleteCard()
        {
            if (cards.Count > 0)
            {
                CardsByCategory[currentCategory].RemoveAt(cardIndex);
                cards = CardsByCategory[currentCategory];
                SwitchCard(1);
            }
            CustomSave();
        }
        

        public void SwitchCard(int steps = 1)
        {
            if (cards.Count > 0)
            {
                Console.WriteLine("Switching");
                cardIndex += steps;
                steps %= cards.Count;
                if (cardIndex < 0) cardIndex += cards.Count;
                cardIndex %= cards.Count;
                OnPropertyChanged("FrontText");
                OnPropertyChanged("BackText");
            }
        } 

        public void ChangeCard (string newFront, string newBack)
        {
            CardsByCategory["Fruit's Colors"][cardIndex] = new Card(newFront, newBack);
            OnPropertyChanged("FrontText");
            OnPropertyChanged("BackText");
            CustomSave();
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
            CustomSave();
        }

        public void EditCategory(string cat)
        {
            CardsByCategory.Add(cat, CardsByCategory[currentCategory]);
            CardsByCategory.Remove(currentCategory);
            ChangeCategory(cat);
            CustomSave();
        }

        public void ChangeCategory (string category)
        {
            cards = CardsByCategory[category];
            
            CurrentCategory = category;
            cardIndex = 0;

            OnPropertyChanged("FrontText");
            OnPropertyChanged("BackText");
        }

        //Had to write our own Serialization because XML somehow does not support serializing dictionaries
        public void Save(string path)
        {
            this.path = path;
            CustomSave();
        }

        public void CustomSave()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<EntryCustom>));
            TextWriter writer = new StreamWriter(path);

            List<EntryCustom> entries = new List<EntryCustom>();
            foreach (string key in CardsByCategory.Keys)
            {
                foreach (Card value in CardsByCategory[key])
                {
                    entries.Add(new EntryCustom(key, value));
                }
            }

            serializer.Serialize(writer, entries);
            writer.Close();
        }

        public static CardModel Read(string filename)
        {
            try
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<EntryCustom>));
                    List<EntryCustom> list = (List<EntryCustom>)serializer.Deserialize(stream);

                    CardModel c = new CardModel();
                    c.CardsByCategory.Clear();

                    foreach (EntryCustom entry in list)
                    {
                        if (!c.CardsByCategory.ContainsKey(entry.Key))
                        {
                            c.CardsByCategory.Add(entry.Key, new List<Card>());
                        }
                        c.CardsByCategory[entry.Key].Add(entry.Value);
                    }
                    return c;
                }
            }
            catch { return null; }
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

    //Custom class to simulate dictionary entry
    public class EntryCustom
    {
        public string Key;
        public Card Value;
        public EntryCustom()
        {
            Key = "";
            Value = new Card();
        }

        public EntryCustom(string key, Card value)
        {
            Key = key;
            Value = value;
        }
    }
}