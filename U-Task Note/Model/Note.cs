using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace U_Task_Note.Model
{
    public class Note : INotifyPropertyChanged
    {
        public int ID { get; set; }
        private string Name;
        private string Text;
        public DateTime CreationDate { get; set; }
        public string TextProperty
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
                OnPropertyChanged(nameof(TextProperty));
            }
        }
        public string NameProperty
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                OnPropertyChanged(nameof(NameProperty));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
