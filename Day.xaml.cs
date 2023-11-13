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
        private static Day nowOpened;
        private DateOnly date;
        Label Representation { get; } = new();
        public Day(DateOnly date) {
            this.date = date;

            Representation.Content = date.Day;
            Representation.MouseDown += this.ShowEvent;
            InitializeComponent();
            
        }
        private void ShowEvent(object sender, MouseButtonEventArgs e) {
            Representation.Background = Brushes.Aqua;
            nowOpened.Representation.Background = Brushes.Transparent;
            nowOpened.Close();
            nowOpened = this;
        }

    }
}
