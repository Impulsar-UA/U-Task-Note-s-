using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using U_Task_Note.Model;

namespace U_Task_Note.ViewModel
{
    public partial class TasksMenuViewModel : INotifyPropertyChanged
    {
        private BaseContext Context = new BaseContext();
        public ObservableCollection<Model.Task> TaskList { get; set; }
        public ObservableCollection<Model.Task> UncompleteTaskList { get; set; }
        public ObservableCollection<Model.Task> СompleteTaskList { get; set; }
        private string _taskText;
        public string TaskText
        {
            get
            {
                return _taskText;
            }
            set
            {
                _taskText = value;
                OnPropertyChanged(nameof(TaskText));
            }
        }

        private string _taskName;
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
        private DateTime? _taskDeadlineTime;
        public DateTime? TaskDeadlineTime
        {
            get
            {
                return _taskDeadlineTime;
            }
            set
            {
                _taskDeadlineTime = value;
                OnPropertyChanged(nameof(TaskDeadlineTime));
            }
        }
        private Priority _taskPriority;
        public Priority TaskPriority
        {
            get
            {
                return _taskPriority;
            }
            set
            {
                _taskPriority = value;
                OnPropertyChanged(nameof(TaskPriority));
            }
        }
        public List<PriorityItem> PriorityItems { get; } = new List<PriorityItem>
        {
            new PriorityItem { Priority = Priority.Низький, Name = "Низький", Color = new SolidColorBrush(Colors.Green) },
            new PriorityItem { Priority = Priority.Середній, Name = "Середній", Color = new SolidColorBrush(Colors.Yellow) },
            new PriorityItem { Priority = Priority.Високий, Name = "Високий", Color = new SolidColorBrush(Colors.Red) }
        };
        public Dictionary<RepeatFrequency, string> RepeatFrequencyNames { get; } = new Dictionary<RepeatFrequency, string>
         {
            { RepeatFrequency.Немає, "Немає" },
            { RepeatFrequency.Щоденно, "Щоденно" },
            { RepeatFrequency.Щотижня, "Щотижня" }
         };
        private DateTime? _taskNoticeTime;
        public DateTime? TaskNoticeTime
        {
            get
            {
                return _taskNoticeTime;
            }
            set
            {
                _taskNoticeTime = value;
                OnPropertyChanged(nameof(TaskNoticeTime));
            }
        }
        private DateTime _taskEndTime;
        public DateTime TaskEndTime
        {
            get
            {
                return _taskEndTime;
            }
            set
            {
                _taskEndTime = value;
                OnPropertyChanged(nameof(TaskEndTime));
            }
        }
        private string _taskCreationDate;
        public string TaskCreationDate
        {
            get
            {
                return _taskCreationDate;
            }
            set
            {
                _taskCreationDate = value;
                OnPropertyChanged(nameof(TaskCreationDate));
            }
        }
        private RepeatFrequency _taskRepeatFrequency;
        public RepeatFrequency TaskRepeatFrequency
        {
            get
            {
                return _taskRepeatFrequency;
            }
            set
            {
                _taskRepeatFrequency = value;
                OnPropertyChanged(nameof(TaskRepeatFrequency));
            }
        }
        private Model.Task _selectedTask;
        public Model.Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                ReverseIsEditing = !_isEditing;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        private bool _reverseIsEditing;
        public bool ReverseIsEditing
        {
            get { return _reverseIsEditing; }
            set
            {
                _reverseIsEditing = value;
                OnPropertyChanged(nameof(ReverseIsEditing));
            }
        }
        private bool _isNoticing;
        public bool IsNoticing
        {
            get { return _isNoticing; }
            set
            {
                _isNoticing = value;
                OnPropertyChanged(nameof(IsNoticing));
            }
        }
        private bool _isDeadline;
        public bool IsDeadline
        {
            get { return _isDeadline; }
            set
            {
                _isDeadline = value;
                OnPropertyChanged(nameof(IsDeadline));
            }
        }



    }
}
