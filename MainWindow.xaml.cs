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
    public partial class MainWindow : Window {
        DateOnly mainDate = new(2015, 1, 1);
        List<Week> weeks = new List<Week>();
        public MainWindow() {
            InitializeComponent();
            string[] days = {"Monday", "Tuesday", "Wednesday", "Thusday", "Friday", "Saturday", "Sunday" };
            for(int i = 0; i < days.Length; i++) {
                var tmp = new Label() {
                    Content = days[i]
                };
                Grid.SetColumn(tmp, i+1);
                Grid.SetRow(tmp, 0);
                main.Children.Add(tmp);
            }
            for(DateOnly i = mainDate; i.Month == mainDate.Month; i.AddDays(7)) {
                weeks.Add(new(i));
            }
            

        }
    }
}
