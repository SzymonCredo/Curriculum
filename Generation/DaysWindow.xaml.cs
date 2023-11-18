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
using System.Windows.Shapes;

namespace Curriculum {
    /// <summary>
    /// Interaction logic for DaysWindow.xaml
    /// </summary>
    public partial class DaysWindow : Viewbox {
        public static StackPanel DaysParent;
        Day root;
        public DaysWindow(Day root) {
            this.root = root;
            InitializeComponent();
            {
                var day = root.date.Day.ToString().Length < 2 ? '0' + root.date.Day.ToString() : root.date.Day.ToString();
                var month = root.date.Month.ToString().Length < 2 ? '0' + root.date.Month.ToString() : root.date.Month.ToString();

                Title.Content = day + "." + month + "." + root.date.Year.ToString();

            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            root.WindowClose();
        }
        public void show() {
            this.Visibility = Visibility.Visible;
            if(!DaysParent.Children.Contains(this))
                DaysParent.Children.Add(this);
        }
        public void close() {
            
            this.Visibility = Visibility.Collapsed;
            if (DaysParent.Children.Contains(this))
                DaysParent.Children.Remove(this);
        }
    }
}
