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
        private DateOnly date;
        public Day(DateOnly date) {
            this.date = date;
            Content = date.Day;
            MouseDown += this.ShowEvent;
            int dayofweek = date.DayOfWeek == 0 ? 6 : (int)date.DayOfWeek - 1;
            Grid.SetColumn(this, (int)dayofweek + 1);

            window = new(this);
        }
        public void WindowClose() {
            if (nowOpened == null)
                return;
            if (nowOpened.IsVisible)
                nowOpened.window.Close();
            nowOpened.Background = Brushes.Transparent;
            nowOpened.window = new(nowOpened);
            nowOpened = null;
        }
        private void ShowEvent(object sender, MouseButtonEventArgs e) {
            if (nowOpened == this)
                return;
            Background = Brushes.Aqua;
            if(nowOpened != null) {
                nowOpened.window.Close();
            }
            window.Show();
            nowOpened = this;
        }

        
    }
}
