using System;
using System.Collections;
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
using System.Xml;
using Curriculum.Events;

namespace Curriculum
{
    /// <summary>
    /// Interaction logic for Day.xaml
    /// </summary>
    public partial class Day : Label {
        
        private static Day? nowOpened = null;
        private DaysWindow window;
        public DateOnly date { get; }
        private int dayofweek;
        public List<CurriculumEvent> events { 
            get {
                List<CurriculumEvent> evlist;
                if(CurriculumEvent.events.TryGetValue(date , out evlist)) {
                    return evlist;
                }
                return new();
            }
        }
        public Day(DateOnly date) {
            this.date = date;
            VerticalAlignment = VerticalAlignment.Center;
            Content = date.Day;
            MouseDown += this.ShowEvent;
            MouseEnter += this.MouseOnEvent;
            MouseLeave += this.MouseLeftEvent;

            dayofweek = date.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)date.DayOfWeek - 1;
            Grid.SetColumn(this, dayofweek + 1);
            // create window
            window = new(this);
        }
        public void WindowClose() {
            if (nowOpened == null)
                return;
            window.close();
            nowOpened.Background = Brushes.Transparent;
            nowOpened = null;
        }
        private void ShowEvent(object sender, EventArgs e) {
            if (nowOpened == this)
                return;
            Background = Brushes.Aqua;
            if(nowOpened != null) {
                nowOpened.WindowClose();
            }
            window.show();
            nowOpened = this;
        }
        private void MouseOnEvent(object sender, EventArgs e) {
            Background = Brushes.Gray;
            Cursor = Cursors.Hand;
        }
        private void MouseLeftEvent(object sender , EventArgs e) {
            Cursor = Cursors.Arrow;
            if(nowOpened == this)
                Background = Brushes.Aqua;
            else
                Background = Brushes.Transparent;
        }

    }
}
