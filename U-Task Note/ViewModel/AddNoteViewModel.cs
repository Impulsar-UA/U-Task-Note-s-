//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using U_Task_Note.Model;
//using U_Task_Note.View;
//using U_Task_Note;
//using System.Collections.ObjectModel;
//using System.Windows.Controls;


//namespace U_Task_Note.ViewModel
//{
//    public class AddNoteViewModel : INotifyPropertyChanged
//    {  
//        public event PropertyChangedEventHandler? PropertyChanged;
//        public void OnPropertyChanged([CallerMemberName] string prop = "")
//        {
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs(prop));
//        }
//        private string NoteText;
//        public string NoteTextProperty
//        {
//            get
//            {
//                return NoteText;
//            }
//            set
//            {
//                NoteText = value;
//                OnPropertyChanged(nameof(NoteTextProperty));
//            }
//        }
//        private string NoteName;
//        public string NoteNameProperty
//        {
//            get
//            {
//                return NoteName;
//            }
//            set
//            {
//                NoteName = value;
//                OnPropertyChanged(nameof(NoteNameProperty));
//            }
//        }
//        private void AddNoteButton_Click(Window CurrentWindow)
//        {
//            if ((NoteName != null) || (NoteText != null))
//            {
//                Note newNote = new Note
//                {
//                    Name = NoteName,
//                    Text = NoteText,
//                    CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0)
//            };
//                using (var context = new BaseContext())
//                {
//                    try
//                    {
//                        context.Notes.Add(newNote);
//                        context.SaveChanges();
//                        MessageBox.Show("Успішно", "Нотатку збережено", MessageBoxButton.OK, MessageBoxImage.Information);
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
//                    }
//                }
//                CurrentWindow.Close();
//            }
//            else
//            {
//                MessageBox.Show("Пусте поле", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }
//        private RelayCommand? AddNoteCommand;
//        public RelayCommand AddNoteCommandProperty
//        {
//            get
//            {
//                return AddNoteCommand ?? (AddNoteCommand = new RelayCommand(obj => AddNoteButton_Click(obj as Window)));
//            }
//        }
//    }
//}
