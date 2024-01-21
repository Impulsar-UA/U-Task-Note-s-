using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using U_Task_Note.Model;

namespace U_Task_Note.ViewModel
{
    public class PriorityItem
    {
        public Priority Priority { get; set; }
        public string Name { get; set; }
        public SolidColorBrush Color { get; set; }
    }
}
