using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Curriculum {
    /// <summary>
    /// Interaction logic for Day.xaml
    /// </summary>
    public partial class Day : Label {
        
        private static Day? nowOpened = null;
        private DaysWindow window;
        public DateOnly date { get; }
        private int dayofweek;
        public Day(DateOnly date) {
            this.date = date;
            VerticalAlignment = VerticalAlignment.Center;
            Content = date.Day;
            MouseDown += this.ShowEvent;
            dayofweek = date.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)date.DayOfWeek - 1;
            Grid.SetColumn(this, dayofweek + 1);

            window = new(this);
        }
        public void WindowClose() {
            if (nowOpened == null)
                return;
            window.close();
            nowOpened.Background = Brushes.Transparent;
            nowOpened = null;
        }
        private void ShowEvent(object sender, MouseButtonEventArgs e) {
            if (nowOpened == this)
                return;
            Background = Brushes.Aqua;
            if(nowOpened != null) {
                nowOpened.WindowClose();
            }
            window.show();
            nowOpened = this;
        }

        
    }
}
