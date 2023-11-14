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
    public partial class DaysWindow : Window {
        Day root;
        public DaysWindow(Day root) {
            this.root = root;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e) {
            root.WindowClose();
        }
    }
}
