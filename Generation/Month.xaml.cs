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

namespace Curriculum.Generation {
    /// <summary>
    /// Interaction logic for Month.xaml
    /// </summary>
    public partial class Month : UserControl {
        DateOnly mainDate;

        private static string[] Months = { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
        List<Day> Days = new();
        public Month(DateOnly mainDate) {
            InitializeComponent();
            this.mainDate = new DateOnly(mainDate.Year, mainDate.Month, 1);


            {// labels for days of week
                main.RowDefinitions.Add(new());
                string[] DaysOfWeek = { "Poniedzi", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
                for (int i = 0; i < DaysOfWeek.Length; i++) {
                    var tmp = new Label() {
                        Content = DaysOfWeek[i]
                    };
                    Grid.SetColumn(tmp, i + 1);
                    Grid.SetRow(tmp, 0);
                    main.Children.Add(tmp);
                }
            }


            int MainDayOfWeek = mainDate.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)mainDate.DayOfWeek - 1; // go to monday
            DateOnly startDate = mainDate.AddDays(-MainDayOfWeek % 7);

            for (int j = 0; startDate.AddDays(j * 7).Month == mainDate.Month || j == 0; j++) { // for week
                main.RowDefinitions.Add(new());
                for (int i = 0; i < 7; i++) { // for day
                    DateOnly current = startDate.AddDays(j * 7 + i);
                    var day = new Day(current);
                    if ((startDate.AddDays(j * 7 + i).Month != mainDate.Month && j != 0) || (j == 0 && current.Day > 7))
                        day.Foreground = Brushes.LightGray;

                    Grid.SetRow(day, j + 1);

                    { // deteremin column
                        int dayofweek = current.DayOfWeek == 0 ? 7 : (int)current.DayOfWeek;
                        Grid.SetColumn(day, dayofweek);
                    }

                    Days.Add(day);
                    main.Children.Add(day);
                }
            }



        }


    }
}
