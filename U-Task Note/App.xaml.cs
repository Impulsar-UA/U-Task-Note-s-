using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using U_Task_Note.View;
using U_Task_Note.ViewModel;
using System.Collections.ObjectModel;
using U_Task_Note.View.Templates;
using System.Windows.Media.Imaging;

namespace U_Task_Note
{
    public partial class App : Application
    {
        private HashSet<int> notifiedTasks = new HashSet<int>();
        private ObservableCollection<Model.Task> DeadlineTaskList { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new TasksMenu();
            MainWindow.Closing += MainWindow_Closing;
            StartBackgroundTask();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            (sender as Window).Hide();
        }
        private Timer _timer;

        private void StartBackgroundTask()
        {
            var now = DateTime.Now;
            var nextMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(1);
            var dueTime = nextMinute - now;
            _timer = new Timer(NotionObserving, null, dueTime, TimeSpan.FromMinutes(1));
        }

        private void NotionObserving(object state)
        {
            var deadlineTasks = VMController.TasksMenuVM.UncompleteTaskList.Where(task => task.DeadlineTime.HasValue && task.DeadlineTime > DateTime.Now).ToList();
            var now = DateTime.Now;
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            foreach (var task in deadlineTasks)
            {
                var notificationTime = VMController.TasksMenuVM.CalculateNotificationTime(task);

                if (notificationTime.HasValue && now >= notificationTime.Value)
                {
                    if (!notifiedTasks.Contains(task.ID))
                    {
                        VMController.TasksMenuVM.SendNotification(task);
                        notifiedTasks.Add(task.ID);
                    }
                }
                else if (notificationTime.HasValue)
                {
                    notifiedTasks.Remove(task.ID);
                }
            }
        }
        public void ExitApplication()
        {
            _timer?.Dispose();
            App.Current.Shutdown();
        }
    }
}
