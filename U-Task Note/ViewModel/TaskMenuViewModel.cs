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
    public class TaskMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void ShowAddTask_Click()
        {
            AddTaskWindow NewTaskWindow = new();
            NewTaskWindow.Show();
        }
        private RelayCommand? ShowAddTaskCommand;
        public RelayCommand ShowAddTaskCommandProperty
        {
            get
            {
                return ShowAddTaskCommand ?? (ShowAddTaskCommand = new RelayCommand(obj => ShowAddTask_Click()));
            }
        }
        
        //private void CopyButton_Click()
        //{
        //    if (OutputText != null)
        //    {
        //        Clipboard.SetText(OutputText);
        //    }
        //}
        //private RelayCommand CopyCommand;
        //    public RelayCommand CopyCommandProperty
        //    {
        //        get
        //        {
        //            return CopyCommand ?? (CopyCommand = new RelayCommand(obj => CopyButton_Click()));
        //        }
        //    }
    }
}
