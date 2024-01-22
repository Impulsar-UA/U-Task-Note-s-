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
using U_Task_Note.View.Templates;
using System.Windows.Media;


namespace U_Task_Note.ViewModel
{
    public partial class TasksMenuViewModel : INotifyPropertyChanged
    {
        private DateTime _keyDay;
        private DateTime _mondayDate;
        public DateTime MondayDate
        {
            get { return _mondayDate; }
            private set
            {
                if (_mondayDate != value)
                {
                    _mondayDate = value;
                    OnPropertyChanged(nameof(MondayDate));
                }
            }
        }

        private DateTime _tuesdayDate;
        public DateTime TuesdayDate
        {
            get { return _tuesdayDate; }
            private set
            {
                if (_tuesdayDate != value)
                {
                    _tuesdayDate = value;
                    OnPropertyChanged(nameof(TuesdayDate));
                }
            }
        }

        private DateTime _wednesdayDate;
        public DateTime WednesdayDate
        {
            get { return _wednesdayDate; }
            private set
            {
                if (_wednesdayDate != value)
                {
                    _wednesdayDate = value;
                    OnPropertyChanged(nameof(WednesdayDate));
                }
            }
        }

        private DateTime _thursdayDate;
        public DateTime ThursdayDate
        {
            get { return _thursdayDate; }
            private set
            {
                if (_thursdayDate != value)
                {
                    _thursdayDate = value;
                    OnPropertyChanged(nameof(ThursdayDate));
                }
            }
        }

        private DateTime _fridayDate;
        public DateTime FridayDate
        {
            get { return _fridayDate; }
            private set
            {
                if (_fridayDate != value)
                {
                    _fridayDate = value;
                    OnPropertyChanged(nameof(FridayDate));
                }
            }
        }

        private DateTime _saturdayDate;
        public DateTime SaturdayDate
        {
            get { return _saturdayDate; }
            private set
            {
                if (_saturdayDate != value)
                {
                    _saturdayDate = value;
                    OnPropertyChanged(nameof(SaturdayDate));
                }
            }
        }

        private DateTime _sundayDate;
        public DateTime SundayDate
        {
            get { return _sundayDate; }
            private set
            {
                if (_sundayDate != value)
                {
                    _sundayDate = value;
                    OnPropertyChanged(nameof(SundayDate));
                }
            }
        }

        private bool _isToday;
        public bool IsToday
        {
            get { return _isToday; }
            set
            {
                if (_isToday != value)
                {
                    _isToday = value;
                    OnPropertyChanged(nameof(IsToday));
                }
            }
        }

        private int _dayOfMonth;
        public int DayOfMonth
        {
            get { return _dayOfMonth; }
            set
            {
                if (_dayOfMonth != value)
                {
                    _dayOfMonth = value;
                    OnPropertyChanged(nameof(DayOfMonth));
                }
            }
        }

        private string _monthName;
        public string MonthName
        {
            get { return _monthName; }
            set
            {
                if (_monthName != value)
                {
                    _monthName = value;
                    OnPropertyChanged(nameof(MonthName));
                }
            }
        }
        public ObservableCollection<Model.Task> MondayTasks { get; private set; }
        public ObservableCollection<Model.Task> TuesdayTasks { get; private set; }
        public ObservableCollection<Model.Task> WednesdayTasks { get; private set; }
        public ObservableCollection<Model.Task> ThursdayTasks { get; private set; }
        public ObservableCollection<Model.Task> FridayTasks { get; private set; }
        public ObservableCollection<Model.Task> SaturdayTasks { get; private set; }
        public ObservableCollection<Model.Task> SundayTasks { get; private set; }

        private void InitializeDatesofDays(DateTime keyDay)
        {
            DayOfWeek currentDay = keyDay.DayOfWeek;
            DateTime startOfWeek = keyDay.AddDays(-(int)currentDay);

            MondayDate = startOfWeek;
            TuesdayDate = startOfWeek.AddDays(1);
            WednesdayDate = startOfWeek.AddDays(2);
            ThursdayDate = startOfWeek.AddDays(3);
            FridayDate = startOfWeek.AddDays(4);
            SaturdayDate = startOfWeek.AddDays(5);
            SundayDate = startOfWeek.AddDays(6);
        }
        private void InitializeWeek()
        {
            _keyDay = DateTime.Today; 
            InitializeDatesofDays(_keyDay);
            LoadWeekTasksLists();
        }
        private void LoadWeekTasksLists()
        {
            MondayTasks = new ObservableCollection<Model.Task>();
            TuesdayTasks = new ObservableCollection<Model.Task>();
            WednesdayTasks = new ObservableCollection<Model.Task>();
            ThursdayTasks = new ObservableCollection<Model.Task>();
            FridayTasks = new ObservableCollection<Model.Task>();
            SaturdayTasks = new ObservableCollection<Model.Task>();
            SundayTasks = new ObservableCollection<Model.Task>();
            var tasksInCurrentWeek = TaskList.Where(task => task.DeadlineTime.HasValue && task.DeadlineTime.Value.Date >= MondayDate && task.DeadlineTime.Value.Date <= SundayDate);
            MondayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == MondayDate.Date));
            TuesdayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == TuesdayDate.Date));
            WednesdayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == WednesdayDate.Date));
            ThursdayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == ThursdayDate.Date));
            FridayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == FridayDate.Date));
            SaturdayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == SaturdayDate.Date));
            SundayTasks = new ObservableCollection<Model.Task>(tasksInCurrentWeek.Where(task => task.DeadlineTime.Value.Date == SundayDate.Date));
        }
        private void UpdateWeekTasksLists()
        {
            MondayTasks.Clear();
            TuesdayTasks.Clear();
            WednesdayTasks.Clear();
            ThursdayTasks.Clear();
            FridayTasks.Clear();
            SaturdayTasks.Clear();
            SundayTasks.Clear();

            var tasksInCurrentWeek = TaskList
                .Where(task => task.DeadlineTime.HasValue && task.DeadlineTime.Value.Date >= MondayDate && task.DeadlineTime.Value.Date <= SundayDate);

            foreach (var task in tasksInCurrentWeek)
            {
                if (task.DeadlineTime.Value.Date == MondayDate.Date)
                    MondayTasks.Add(task);
                else if (task.DeadlineTime.Value.Date == TuesdayDate.Date)
                    TuesdayTasks.Add(task);
                else if (task.DeadlineTime.Value.Date == WednesdayDate.Date)
                    WednesdayTasks.Add(task);
                else if (task.DeadlineTime.Value.Date == ThursdayDate.Date)
                    ThursdayTasks.Add(task);
                else if (task.DeadlineTime.Value.Date == FridayDate.Date)
                    FridayTasks.Add(task);
                else if (task.DeadlineTime.Value.Date == SaturdayDate.Date)
                    SaturdayTasks.Add(task);
                else if (task.DeadlineTime.Value.Date == SundayDate.Date)
                    SundayTasks.Add(task);
            }
        }
        public void NextWeek()
        {
            _keyDay = _keyDay.AddDays(7);
            InitializeDatesofDays(_keyDay);
            UpdateWeekTasksLists();
        }
        private RelayCommand? _nextWeekCommand;
        public RelayCommand NextWeekCommand
        {
            get
            {
                return _nextWeekCommand ?? (_nextWeekCommand = new RelayCommand(obj => NextWeek()));
            }
        }
        public void PreviousWeek()
        {
            _keyDay = _keyDay.AddDays(-7);
            InitializeDatesofDays(_keyDay);
            UpdateWeekTasksLists();
        }
        private RelayCommand? _previousWeekCommand;
        public RelayCommand PreviousWeekCommand
        {
            get
            {
                return _previousWeekCommand ?? (_previousWeekCommand = new RelayCommand(obj => PreviousWeek()));
            }
        }
    }
}
