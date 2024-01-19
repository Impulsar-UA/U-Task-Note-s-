using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace U_Task_Note.Model
{
    public class Task : INotifyPropertyChanged
    {
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _deadlineTime;
        public DateTime? DeadlineTime
        {
            get { return _deadlineTime; }
            set
            {
                if (_deadlineTime != value)
                {
                    _deadlineTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private Priority _priority;
        public Priority Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _noticeTime;
        public DateTime? NoticeTime
        {
            get { return _noticeTime; }
            set
            {
                if (_noticeTime != value)
                {
                    _noticeTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (_creationDate != value)
                {
                    _creationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private RepeatFrequency _repeatFrequency;
        public RepeatFrequency RepeatFrequency
        {
            get { return _repeatFrequency; }
            set
            {
                if (_repeatFrequency != value)
                {
                    _repeatFrequency = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
