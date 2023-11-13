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

namespace Curriculum {
    /// <summary>
    /// Interaction logic for Day.xaml
    /// </summary>
    public partial class Day : Window {
        private static Day? nowOpened = null;
        private DateOnly date;
        public Label Representation { get; } = new();
        public Day(DateOnly date) {
            this.date = date;

            Representation.Content = date.Day;
            Representation.MouseDown += this.ShowEvent;
            int dayofweek = date.DayOfWeek == 0 ? 6 : (int)date.DayOfWeek - 1;
            Grid.SetColumn(Representation, (int)dayofweek+1);
            InitializeComponent();
        }
        public Day(Day other) {
            this.date = other.date;
            Representation = other.Representation;
            InitializeComponent();
        }
        private void ShowEvent(object sender, MouseButtonEventArgs e) {
            Representation.Background = Brushes.Aqua;
            if(nowOpened != null) {
                nowOpened.Representation.Background = Brushes.Transparent;
                nowOpened.Close();
                nowOpened = new(nowOpened); // make him able to open agian
            }
            this.Show();
            nowOpened = this;
        }

        private void Window_Closed(object sender, EventArgs e) {
            nowOpened = new(nowOpened);
            nowOpened = null;
        }
    }
}
