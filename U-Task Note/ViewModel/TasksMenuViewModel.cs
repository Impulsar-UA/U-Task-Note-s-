using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using U_Task_Note.Model;
using U_Task_Note.View;
using U_Task_Note;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using U_Task_Note.View.Templates;
using System.Windows.Media;


namespace U_Task_Note.ViewModel
{
    public class TasksMenuViewModel : INotifyPropertyChanged
    {
        public TasksMenuViewModel()
        {
            Context.Tasks.Load();
            TaskList = Context.Tasks.Local.ToObservableCollection();
            IsEditing = false;

        }
        private BaseContext Context = new BaseContext();
        public ObservableCollection<Model.Task> TaskList { get; set; }
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
        private void ShowAddTask()
        {
            AddTaskWindow NewTaskWindow = new();
            TaskText = null;
            TaskName = null;
            TaskDeadlineTime = null;
            TaskNoticeTime = null;
            NewTaskWindow.Show();
        }
        private RelayCommand? ShowAddTaskCommand;
        public RelayCommand ShowAddTaskCommandProperty
        {
            get
            {
                return ShowAddTaskCommand ?? (ShowAddTaskCommand = new RelayCommand(obj => ShowAddTask()));
            }
        }
        private void AddTaskButton(Window CurrentWindow)
        {
            if ((TaskName != null) || (TaskText != null))
            {
                if (TaskDeadlineTime == DateTime.MinValue) { TaskDeadlineTime = null; }
                if (TaskNoticeTime == DateTime.MinValue) { TaskNoticeTime = null; }

                //TaskNoticeTime = (yourDatePickerForNotice.SelectedDate == DateTime.MinValue) ? (DateTime?)null : yourDatePickerForNotice.SelectedDate;
                Model.Task newTask = new Model.Task
                {
                    Name = TaskName,
                    Text = TaskText,
                    CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0),
                    Priority = TaskPriority,
                    DeadlineTime = TaskDeadlineTime,
                    NoticeTime = TaskNoticeTime,
                    RepeatFrequency = TaskRepeatFrequency
                };
                try
                {
                    Context.Tasks.Add(newTask);
                    Context.SaveChanges();
                    MessageBox.Show("Успішно", "Завдання збережено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                CurrentWindow.Close();
            }
            else
            {
                MessageBox.Show("Пусте поле", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private RelayCommand? _addTaskCommand;
        public RelayCommand AddTaskCommandProperty
        {
            get
            {
                return _addTaskCommand ?? (_addTaskCommand = new RelayCommand(obj => AddTaskButton(obj as Window)));
            }
        }
        private void ShowTask()
        {
            ShowTaskWindow TaskWindow = new();
            IsEditing = false;
            TaskCreationDate = SelectedTask.CreationDate.ToString("yyyy-MM-dd HH:mm");
            TaskText = SelectedTask.Text;
            TaskName = SelectedTask.Name;
            TaskPriority = (Priority)SelectedTask.Priority;
            TaskDeadlineTime = SelectedTask.DeadlineTime;
            TaskNoticeTime = SelectedTask.NoticeTime;
            TaskRepeatFrequency = (RepeatFrequency)SelectedTask.RepeatFrequency;
            if (SelectedTask.DeadlineTime == null) { IsDeadline= false; }
            if (SelectedTask.NoticeTime == null) { IsNoticing = false; }
            TaskWindow.ShowDialog();
        }
        private RelayCommand? _showTaskCommand;
        public RelayCommand ShowTaskCommand
        {
            get
            {
                return _showTaskCommand ?? (_showTaskCommand = new RelayCommand(obj => ShowTask()));
            }
        }
        private void EndEditing()
        {
            IsEditing = false;
            TaskName = SelectedTask.Name;
            TaskText = SelectedTask.Text;
        }
        private RelayCommand? _endEditingCommand;
        public RelayCommand EndEditingCommand
        {
            get
            {
                return _endEditingCommand ?? (_endEditingCommand = new RelayCommand(obj => EndEditing()));
            }
        }
        private void CancelEditingWindow(Window CurrentWindow)
        {
            if (IsEditing == true)
            {
                var result = MessageBox.Show("Зміни не збережені", "Зберегти зміни?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveEditTask();
                    CurrentWindow.Close();
                }
                else
                {
                    if (result == MessageBoxResult.No)
                    {
                        EndEditing();
                        CurrentWindow.Close();
                    }
                }
            }
            else
            {
                CurrentWindow.Close();
            }
        }
        private RelayCommand? _cancelEditingWindow;
        public RelayCommand CancelEditingWindowCommand
        {
            get
            {
                return _cancelEditingWindow ?? (_cancelEditingWindow = new RelayCommand(obj => CancelEditingWindow(obj as Window)));
            }
        }
        private void StartEditing()
        {
            IsEditing = true;
        }
        private RelayCommand? _startEditingCommand;
        public RelayCommand StartEditingCommand
        {
            get
            {
                return _startEditingCommand ?? (_startEditingCommand = new RelayCommand(obj => StartEditing()));
            }
        }
        private void SaveEditTask()
        {
            try
            {
                Model.Task TaskToUpdate = Context.Tasks.Find(SelectedTask.ID);
                if (TaskToUpdate != null)
                {
                    TaskToUpdate.Name = TaskName;
                    TaskToUpdate.Text = TaskText;
                    Context.SaveChanges();
                    MessageBox.Show("Успішно", "Зміни збережені", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsEditing = false;
                }
                else
                {
                    MessageBox.Show("Помилка", "Нотатку не знайдено", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private RelayCommand? _saveEditTaskCommand;
        public RelayCommand SaveEditTaskCommand
        {
            get
            {
                return _saveEditTaskCommand ?? (_saveEditTaskCommand = new RelayCommand(obj => SaveEditTask()));
            }
        }
        private void DeleteTask(Window CurrentWindow)
        {
            var result = MessageBox.Show("Підтвердження видалення", "Видалити завдання? Відновити його буде неможливо!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (IsEditing == true) { EndEditing(); }
                try
                {
                    Model.Task TaskToDelete = Context.Tasks.Find(SelectedTask.ID);
                    if (TaskToDelete != null)
                    {
                        Context.Tasks.Remove(TaskToDelete);
                        Context.SaveChanges();
                        MessageBox.Show("Успішно", "Завдання видалено", MessageBoxButton.OK, MessageBoxImage.Information);
                        CurrentWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Помилка", "Завдання не знайдено", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private RelayCommand? _deleteTaskCommand;
        public RelayCommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand(obj => DeleteTask(obj as Window)));
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
