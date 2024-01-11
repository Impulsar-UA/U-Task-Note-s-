using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using U_Task_Note.View;

namespace U_Task_Note.ViewModel
{
    public static class GeneralNavigationCommands
    {
        private static void GoToNotes_Click(Window CurrentWindow)
        {
            CurrentWindow.Content = new NotesMenu();
        }
        private static RelayCommand? GoToNotesCommand;
        public static RelayCommand GoToNotesCommandProperty
        {
            get
            {
                return GoToNotesCommand ?? (GoToNotesCommand = new RelayCommand(obj => GoToNotes_Click(obj as Window)));
            }
        }
        private static void GoToHistory_Click(Window CurrentWindow)
        {
            CurrentWindow.Content = new HistoryMenu();
        }
        private static RelayCommand? GoToHistoryCommand;
        public static RelayCommand GoToHistoryCommandProperty
        {
            get
            {
                return GoToHistoryCommand ?? (GoToHistoryCommand = new RelayCommand(obj => GoToHistory_Click(obj as Window)));
            }
        }
        private static void GoToOverdueTasks_Click(Window CurrentWindow)
        {
            CurrentWindow.Content = new OverdueTasksMenu();
        }
        private static RelayCommand? GoToOverdueTasksCommand;
        public static RelayCommand GoToOverdueTasksCommandProperty
        {
            get
            {
                return GoToOverdueTasksCommand ?? (GoToOverdueTasksCommand = new RelayCommand(obj => GoToOverdueTasks_Click(obj as Window)));
            }
        }
        private static void GoToMainTaskMenu_Click(Window CurrentWindow)
        {
            TasksMenu TaskWindow = new();
            CurrentWindow.Content = TaskWindow.Content;
            TaskWindow.Close();
        }
        private static RelayCommand? GoToMainTaskMenuCommand;
        public static RelayCommand GoToMainTaskMenuCommandProperty
        {
            get
            {
                return GoToMainTaskMenuCommand ?? (GoToMainTaskMenuCommand = new RelayCommand(obj => GoToMainTaskMenu_Click(obj as Window)));
            }
        }
        private static void CloseCurrentWindow_Click(Window CurrentWindow)
        {
            CurrentWindow.Close();
        }
        private static RelayCommand? CloseCurrentWindowCommand;
        public static RelayCommand CloseCurrentWindowCommandProperty
        {
            get
            {
                return CloseCurrentWindowCommand ?? (CloseCurrentWindowCommand = new RelayCommand(obj => CloseCurrentWindow_Click(obj as Window)));
            }
        }
        private static void GoToTaskCalendarMenu(Window CurrentWindow)
        {
            CurrentWindow.Content = new TaskCalendar();
        }
        private static RelayCommand? _goToTaskCalendarCommand;
        public static RelayCommand GoToTaskCalendarCommand
        {
            get
            {
                return _goToTaskCalendarCommand ?? (_goToTaskCalendarCommand = new RelayCommand(obj => GoToTaskCalendarMenu(obj as Window)));
            }
        }

    }
}
