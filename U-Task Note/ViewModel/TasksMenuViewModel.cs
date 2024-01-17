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
       
   
    }
}
