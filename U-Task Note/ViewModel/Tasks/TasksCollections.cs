using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using U_Task_Note.Model;
using U_Task_Note.View.Templates;

namespace U_Task_Note.ViewModel
{
    public partial class TasksMenuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Model.Task> TaskList { get; set; }
        public ObservableCollection<Model.Task> UncompleteTaskList { get; set; }
        public ObservableCollection<Model.Task> СompleteTaskList { get; set; }
        public ObservableCollection<Model.Task> OverdueTaskList { get; set; }
        private void UpdateLists()
        {
            Context.Tasks.Load();
            TaskList = Context.Tasks.Local.ToObservableCollection();
            UncompleteTaskList = new ObservableCollection<Model.Task>(TaskList.Where(task => task.EndTime == null));
            СompleteTaskList = new ObservableCollection<Model.Task>(TaskList.Where(task => task.EndTime != null));
            OverdueTaskList = new ObservableCollection<Model.Task>(TaskList.Where(task => task.DeadlineTime.HasValue && task.DeadlineTime < DateTime.Now));
        }
        private void SortCompleteUncompleteLists() 
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
                if (task.DeadlineTime.HasValue && task.DeadlineTime < DateTime.Now)
                {
                    OverdueTaskList.Add(task);
                }
            }
        
        }
        private void TaskList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateWeekTasksLists();
            SortCompleteUncompleteLists();
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
    }
}
