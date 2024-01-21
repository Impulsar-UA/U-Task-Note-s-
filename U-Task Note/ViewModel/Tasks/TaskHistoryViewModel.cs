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
        private void CleanTaskHistory()
        {
            var result = MessageBox.Show("Підтвердження видалення", "Видалити історію? Відновити буде неможливо!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (СompleteTaskList.Count != 0)
                    {
                        foreach (var task in СompleteTaskList.ToList())
                        {
                            Model.Task taskToDelete = Context.Tasks.Find(task.ID);

                            if (taskToDelete != null)
                            {
                                Context.Tasks.Remove(taskToDelete);
                            }
                        }
                        Context.SaveChanges();
                        СompleteTaskList.Clear();
                        Context.SaveChanges();
                        MessageBox.Show("Успішно", "Історія завдання очищена", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Помилка", "Завданнь не знайдено", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private RelayCommand? _cleanTaskHistoryCommand;
        public RelayCommand CleanTaskHistoryCommand
        {
            get
            {
                return _cleanTaskHistoryCommand ?? (_cleanTaskHistoryCommand = new RelayCommand(obj => CleanTaskHistory()));
            }
        }
    }
}
