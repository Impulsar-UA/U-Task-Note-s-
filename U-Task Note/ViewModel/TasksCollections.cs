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
        public ObservableCollection<Model.Task> TaskList { get; set; }
        public ObservableCollection<Model.Task> UncompleteTaskList { get; set; }
        public ObservableCollection<Model.Task> СompleteTaskList { get; set; }
       
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
