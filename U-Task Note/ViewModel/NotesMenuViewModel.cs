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

namespace U_Task_Note.ViewModel
{
    public class NotesMenuViewModel : INotifyPropertyChanged
    {
        public NotesMenuViewModel()
        {
            Context.Notes.Load();
            NotesList = Context.Notes.Local.ToObservableCollection();
            IsEditing = false;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private BaseContext Context = new BaseContext();
        public ObservableCollection<Note> NotesList { get; set; }
        private string _noteText;
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;
                OnPropertyChanged(nameof(NoteText));
            }
        }
        private string _noteName;
        public string NoteName
        {
            get
            {
                return _noteName;
            }
            set
            {
                _noteName = value;
                OnPropertyChanged(nameof(NoteName));
            }
        }
        private Note _selectedNote;
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }
        private string _noteCreationDate;
        public string NoteCreationDate
        {
            get
            {
                return _noteCreationDate;
            }
            set
            {
                _noteCreationDate = value;
                OnPropertyChanged(nameof(NoteCreationDate));
            }
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                ReverseIsEditing = !_isEditing;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        private bool _reverseIsEditing;
        public bool ReverseIsEditing
        {
            get { return _reverseIsEditing; }
            set
            {
                _reverseIsEditing = value;
                OnPropertyChanged(nameof(ReverseIsEditing));
            }
        }

        //private bool _progressPoint;
        //public bool ProgressPoint
        //{
        //    get { return _progressPoint; }
        //    set
        //    {
        //        _progressPoint = value;
        //        OnPropertyChanged(nameof(ProgressPoint));
        //    }
        //}
        private void ShowAddNote()
        {
            AddNoteWindow NewNoteWindow = new();
            NoteText = null;
            NoteName = null;
            NewNoteWindow.ShowDialog();
        }
        private RelayCommand? _showAddNoteCommand;
        public RelayCommand ShowAddNoteCommandProperty
        {
            get
            {
                return _showAddNoteCommand ?? (_showAddNoteCommand = new RelayCommand(obj => ShowAddNote()));
            }
        }
        private void AddNoteButton(Window CurrentWindow)
        {
            if ((NoteName != null) || (NoteText != null))
            {
                Note newNote = new Note
                {
                    Name = NoteName,
                    Text = NoteText,
                    CreationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0)
                };
                try
                {
                    Context.Notes.Add(newNote);
                    Context.SaveChanges();
                    MessageBox.Show("Успішно", "Нотатку збережено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                CurrentWindow.Close();
            }
            else
            {
                MessageBox.Show("Пусте поле", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private RelayCommand? _addNoteCommand;
        public RelayCommand AddNoteCommandProperty
        {
            get
            {
                return _addNoteCommand ?? (_addNoteCommand = new RelayCommand(obj => AddNoteButton(obj as Window)));
            }
        }
        private void ShowNote()
        {
            ShowNoteWindow NoteWindow = new();
            NoteCreationDate = SelectedNote.CreationDate.ToString("yyyy-MM-dd HH:mm");
            NoteText = SelectedNote.Text;
            NoteName = SelectedNote.Name;
            NoteWindow.ShowDialog();
        }
        private RelayCommand? _showNoteCommand;
        public RelayCommand ShowNoteCommandProperty
        {
            get
            {
                return _showNoteCommand ?? (_showNoteCommand = new RelayCommand(obj => ShowNote()));
            }
        }
        private void EndEditing()
        {
            IsEditing = false;
            NoteName = SelectedNote.Name;
            NoteText = SelectedNote.Text;
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
                    SaveEditNote();
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
        private void SaveEditNote()
        {
            try
            {
                Note noteToUpdate = Context.Notes.Find(SelectedNote.ID);
                if (noteToUpdate != null)
                {
                    noteToUpdate.Name = NoteName;
                    noteToUpdate.Text = NoteText;
                    Context.SaveChanges();
                    MessageBox.Show("Успішно", "Зміни збережені", MessageBoxButton.OK, MessageBoxImage.Information);
                    IsEditing = false;
                }
                else
                {
                    MessageBox.Show("Помилка", "Нотатку не знайдено", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private RelayCommand? _saveEditNoteCommand;
        public RelayCommand SaveEditNoteCommand
        {
            get
            {
                return _saveEditNoteCommand ?? (_saveEditNoteCommand = new RelayCommand(obj => SaveEditNote()));
            }
        }
        private void DeleteNote(Window CurrentWindow)
        {
            var result = MessageBox.Show("Підтвердження видалення", "Видалити нотатку? Відновити її буде неможливо!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (IsEditing == true) { EndEditing(); }
                try
                {
                    Note noteToDelete = Context.Notes.Find(SelectedNote.ID);
                    if (noteToDelete != null)
                    {
                        Context.Notes.Remove(noteToDelete);
                        Context.SaveChanges();
                        MessageBox.Show("Успішно", "Нотатку видалено", MessageBoxButton.OK, MessageBoxImage.Information);
                        CurrentWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Помилка", "Нотатку не знайдено", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка", $"Помилка: {ex.Message}", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private RelayCommand? _deleteNoteNoteCommand;
        public RelayCommand DeleteNoteNoteCommand
        {
            get
            {
                return _deleteNoteNoteCommand ?? (_deleteNoteNoteCommand = new RelayCommand(obj => DeleteNote(obj as Window)));
            }
        }

    }
}
