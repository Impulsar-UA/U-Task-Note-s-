using System;
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
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow()
        {
            DataContext = VMController.TaskMenuVM;
            InitializeComponent();
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Enter)
                {
                    textBox.AppendText("\n");
                    textBox.SelectionStart += 1;
                }
            }
        }

    }
}
