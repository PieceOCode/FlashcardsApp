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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            Console.WriteLine(propertyName + "Changed!");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
