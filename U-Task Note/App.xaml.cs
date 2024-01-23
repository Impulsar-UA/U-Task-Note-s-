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
            _timer = new Timer(NotionObserving, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private void NotionObserving(object state)
        {
            var deadlineTasks = VMController.TasksMenuVM.TaskList.Where(task => task.DeadlineTime.HasValue && task.DeadlineTime > DateTime.Now).ToList();
            var now = DateTime.Now;

            foreach (var task in deadlineTasks)
            {
                var notificationTime = VMController.TasksMenuVM.CalculateNotificationTime(task);
                if (notificationTime.HasValue)
                {
                    // Проверяем, что время уведомления наступило, но еще не прошла следующая минута
                    if (notificationTime.Value <= now && notificationTime.Value > now.AddMinutes(-1))
                    {
                        //SendNotification(task);
                    }
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
