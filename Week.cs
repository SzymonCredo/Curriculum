using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curriculum {
    internal class Week {
        List<Day> days = new();
        public Week(DateOnly date) {
            DateOnly start_day = date.AddDays(-(int)date.DayOfWeek % 7+1);
            for(DateOnly i = start_day; !i.Equals(start_day.AddDays(7)); i.AddDays(1)){
                var day = new Day(i);
                days.Add(day);
            }
        }
    }
}
