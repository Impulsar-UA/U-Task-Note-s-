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


namespace U_Task_Note.ViewModel
{
    public partial class TasksMenuViewModel : INotifyPropertyChanged
    {
        private void ShowTaskNoEdit()
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
            if (SelectedTask.DeadlineTime == null) { IsDeadline = false; }
            if (SelectedTask.NoticeTime == null) { IsNoticing = false; }
            TaskWindow.ShowDialog();
        }
        private RelayCommand? _showTaskNoEditCommand;
        public RelayCommand ShowTaskNoEditCommand
        {
            get
            {
                return _showTaskNoEditCommand ?? (_showTaskNoEditCommand = new RelayCommand(obj => ShowTaskNoEdit()));
            }
        }
    }
}
