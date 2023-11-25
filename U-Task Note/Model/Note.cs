using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace U_Task_Note.Model
{
    public class Note : INotifyPropertyChanged
    {
        public int ID { get; set; }
        private string _name;
        private string _text;
        public DateTime CreationDate { get; set; }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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
