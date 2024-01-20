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
    public partial class TasksMenuViewModel : INotifyPropertyChanged
    {
        public TasksMenuViewModel()
        {
            IsEditing = false;
            UpdateLists();
            TaskList.CollectionChanged += TaskList_CollectionChanged;
        }
        private void UpdateLists()
        {
            Context.Tasks.Load();
            TaskList = Context.Tasks.Local.ToObservableCollection();
            UncompleteTaskList = new ObservableCollection<Model.Task>(TaskList.Where(task => task.EndTime == null));
            СompleteTaskList = new ObservableCollection<Model.Task>(TaskList.Where(task => task.EndTime != null));         
        }
        private void TaskList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UncompleteTaskList.Clear();
            СompleteTaskList.Clear();

            foreach (var task in TaskList)
            {
                if (task.EndTime == null)
                {
                    UncompleteTaskList.Add(task);
                }
                else
                {
                    СompleteTaskList.Add(task);
                }
            }
        }

        private void AddTask(Window CurrentWindow)
        {
            if ((TaskName != null) && (TaskText != null))
            {
                if (TaskDeadlineTime == DateTime.MinValue) { TaskDeadlineTime = null; }
                if (TaskNoticeTime == DateTime.MinValue) { TaskNoticeTime = null; }
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
                return _addTaskCommand ?? (_addTaskCommand = new RelayCommand(obj => AddTask(obj as Window)));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
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

        private void CompleteTask(Window CurrentWindow)
        {
            try
            {
                Model.Task TaskToUpdate = Context.Tasks.Find(SelectedTask.ID);
                if (TaskToUpdate != null)
                {
                    TaskToUpdate.EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    Context.SaveChanges();
                    СompleteTaskList.Add(SelectedTask);
                    UncompleteTaskList.Remove(SelectedTask);
                    MessageBox.Show("Успішно", "Завдання виконано", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private RelayCommand? _completeTaskCommand;
        public RelayCommand CompleteTaskCommand
        {
            get
            {
                return _completeTaskCommand ?? (_completeTaskCommand = new RelayCommand(obj => CompleteTask(obj as Window)));
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
                    TaskToUpdate.Priority = TaskPriority;
                    TaskToUpdate.DeadlineTime = TaskDeadlineTime;
                    TaskToUpdate.NoticeTime = TaskNoticeTime;
                    TaskToUpdate.RepeatFrequency = TaskRepeatFrequency;
                    Context.SaveChanges();
                    UpdateLists();
                    MessageBox.Show("Успішно", "Зміни збережені", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsEditing = false;
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
        private RelayCommand? _saveEditTaskCommand;
        public RelayCommand SaveEditTaskCommand
        {
            get
            {
                return _saveEditTaskCommand ?? (_saveEditTaskCommand = new RelayCommand(obj => SaveEditTask()));
            }
        }
        private void ShowAddTask()
        {
            AddTaskWindow NewTaskWindow = new();
            TaskText = null;
            TaskName = null;
            TaskDeadlineTime = null;
            TaskNoticeTime = null;
            IsNoticing = false;
            IsDeadline = false;
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
        private void ShowTask()
        {
            if (SelectedTask.EndTime == null) 
            { 
                ShowTaskEditable(); 
            }
            else 
            {
                ShowTaskNoEditable();
            }
        }
        private RelayCommand? _showTaskCommand;
        public RelayCommand ShowTaskCommand
        {
            get
            {
                return _showTaskCommand ?? (_showTaskCommand = new RelayCommand(obj => ShowTask()));
            }
        }
        private void ShowTaskNoEditable()
        {
            ShowTaskNoEditWindow TaskWindow = new();
            IsEditing = false;
            TaskCreationDate = SelectedTask.CreationDate.ToString("yyyy-MM-dd HH:mm");
            TaskText = SelectedTask.Text;
            TaskName = SelectedTask.Name;
            TaskPriority = (Priority)SelectedTask.Priority;
            TaskDeadlineTime = SelectedTask.DeadlineTime;
            TaskNoticeTime = SelectedTask.NoticeTime;
            TaskRepeatFrequency = (RepeatFrequency)SelectedTask.RepeatFrequency;
            if (SelectedTask.DeadlineTime == null) { IsDeadline = false; } else { IsDeadline = true; }
            if (SelectedTask.NoticeTime == null) { IsNoticing = false; } else { IsNoticing = true; }
            TaskWindow.ShowDialog();
        }
        private void ShowTaskEditable()
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
            if (SelectedTask.DeadlineTime == null) { IsDeadline = false; } else { IsDeadline = true; }
            if (SelectedTask.NoticeTime == null) { IsNoticing = false; } else { IsNoticing = true; }
            TaskWindow.ShowDialog();
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
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
