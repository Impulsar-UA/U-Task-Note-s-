﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using U_Task_Note.ViewModel;

namespace U_Task_Note.View
{ 
    public partial class TaskCalendar : Page
    {
        public TaskCalendar()
        {
            DataContext = VMController.TasksMenuVM;
            InitializeComponent();
        }       
    }
}