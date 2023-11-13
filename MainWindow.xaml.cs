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


            MainGenerate();


        }
        private void MainGenerate() {
            main.RowDefinitions.Add(new());
            string[] days = { "POniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
            for (int i = 0; i < days.Length; i++) {
                var tmp = new Label() {
                    Content = days[i]
                };
                Grid.SetColumn(tmp, i + 1);
                Grid.SetRow(tmp, 0);
                main.Children.Add(tmp);
            }
            for (DateOnly i = mainDate; i.Month == mainDate.Month; i = i.AddDays(7)) {
                main.RowDefinitions.Add(new());
                Week tmp = new(i, main);
                weeks.Add(tmp);
            }
        }
        private void MainClear() {
            main.Children.Clear();
            main.RowDefinitions.Clear();
        
        }
        private static string[] Months = {"Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "LIstopad", "Grudzień"};

        private void RightButton(object sender, RoutedEventArgs e) {
            MainClear();
            mainDate = mainDate.AddMonths(1);
            title.Content = mainDate.Year.ToString()+" "+Months[mainDate.Month-1]; 
            MainGenerate();
        }
        private void LeftButton(object sender, RoutedEventArgs e) {
            MainClear();
            mainDate = mainDate.AddMonths(-1);
            title.Content = mainDate.Year.ToString() + " " + Months[mainDate.Month - 1];
            MainGenerate();
        }
    }
}
