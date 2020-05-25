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

            List<Card> characters = new List<Card>();
            characters.Add(new Card("Raistlin Majere", "The setting of Dragonlance, world of Krynn."));
            characters.Add(new Card("Strahd von Zarovich", "The setting of Ravenloft, Castle Ravenloft."));
            characters.Add(new Card("Volothamp Geddarm", "The setting of Forgotten Realms, lives in Waterdeep."));
            characters.Add(new Card("Melf", "The setting of the World of Greyhawk, elven kingdom of Celene."));

            CardsByCategory.Add("Where Is This Iconic Character From?", characters);


            List<Card> creatures = new List<Card>();
            creatures.Add(new Card("Magmin", "Magmins are also known as magmen. They are fire elementals summoned and bound to the Material plane by magic. They crave combustion, and are known for being unpredictibly explosive."));
            creatures.Add(new Card("Ettin", "Ettins are giants with two heads that are capable of independednt thought and can each control one arm for attacking."));
            creatures.Add(new Card("Hellfire Engine", "Hellfire engines are enormous living machines used by devils in the Nine Hells. They are designed to mow down large numbers of creatures at a time."));
            creatures.Add(new Card("Planetar", "Planetars are an order of angels inhabiting the Celestial Planes. They consider themselves defenders of truth and avengers of the fallen righteous. They often serve as messengers of divine beings"));

            CardsByCategory.Add("What Is This Creature?", creatures);

            List<Card> spells = new List<Card>();
            spells.Add(new Card("Abjuration", "Abjuration spells consist mainly of protective spells and barriers."));
            spells.Add(new Card("Evocation", "Evocation spells focus on the manipulation of energry and releasing great destructive forces."));
            spells.Add(new Card("Transmutation", "Transmutation spells change the properties of creature, objects or conditions."));
            spells.Add(new Card("Enchantment", "Enchantment spells affect the minds of others, influencing or controlling their behaviour."));
            spells.Add(new Card("Conjuration", "Conjuration spells manifest creatures, objects or some form of energy to you."));
            spells.Add(new Card("Illusion", "Illusion spells decieve the senses and minds of others."));
            spells.Add(new Card("Divination", "Divination spells enable you to gain knowledge and find hidden things."));
            spells.Add(new Card("Necromancy", "Necromancy spells harness the power of death, using it to cause harm or control the undead."));

            CardsByCategory.Add("Schools of Magic", spells);

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