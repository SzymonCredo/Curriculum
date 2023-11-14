using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Curriculum {
    internal class Week {
        List<Day> days = new();
        public Week(DateOnly date, int nr, Grid? root = null) {
            DateOnly start_date = date.AddDays(-(int)date.DayOfWeek % 7+1);
            for(int i = 0; i < 7;i++){
                DateOnly core_day = start_date.AddDays(i);
                var day = new Day(core_day);
                if(core_day.Month != date.Month)
                    day.Foreground = Brushes.LightGray;
                
                
                Grid.SetRow(day, nr + 1);
                days.Add(day);
                if(root != null) {
                    root.Children.Add(day);
                }
            }

            
        }
        
    }
}
