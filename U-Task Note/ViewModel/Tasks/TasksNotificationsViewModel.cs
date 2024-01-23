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
using Microsoft.Toolkit.Uwp.Notifications;



namespace U_Task_Note.ViewModel
{
    public partial class TasksMenuViewModel : INotifyPropertyChanged
    {
        public void SendNotification(Model.Task task)
        {
            var icon = new Uri("pack://application:,,,/U-Task Note;component/View/Tasks/icon.ico");
            var notification = new ToastContentBuilder();
            notification.AddText($"Нагадування про завдання {task.Name}");
            notification.Show();
        }
        public DateTime? CalculateNotificationTime(Model.Task task)
        {
            if (task.DeadlineTime == null || task.NoticeTime == null)
            {
                return null;
            }
            TimeSpan noticeTimeSpan = task.NoticeTime.Value.TimeOfDay;
            DateTime notificationTime = task.DeadlineTime.Value - noticeTimeSpan;
            notificationTime = new DateTime(notificationTime.Year, notificationTime.Month, notificationTime.Day, notificationTime.Hour, notificationTime.Minute, 0);
            return notificationTime;
        }
    }
}
