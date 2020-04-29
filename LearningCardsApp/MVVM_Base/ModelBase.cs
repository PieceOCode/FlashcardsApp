using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Xml.Serialization;

namespace LearningCardsApp
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected string filename;
        public string Filename { get => filename; set => filename = value; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Serialize to an already set path
        public void Save()
        {
            if(Filename!= null)
            {
                Save(Filename);
            } else
            {
                throw new Exception("No filename set for Model class. Either load object from memory or set a filename.");
            }
        }

        //Serialize Object to into a xml file at location path
        public void Save(string fn)
        {
            this.Filename = fn;
            using (var writer = new StreamWriter(fn))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }

        }

        public static ModelType Load<ModelType>(string fn) where ModelType : ModelBase
        {
            
            try 
            {
                //FileStream stream = File.OpenRead(fn)
                using (var reader = new StreamReader(fn))
                {
                    var serializer = new XmlSerializer(typeof(ModelType));
                    ModelType m = serializer.Deserialize(reader) as ModelType;
                    return m;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
