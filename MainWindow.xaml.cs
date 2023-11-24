using Curriculum.Events;
using Curriculum.Generation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Curriculum {
    public partial class MainWindow : Window {

        public DateOnly mainDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);

        private Month? month;
        private Week? week;


        public MainWindow() {
            ImportEvents();
            InitializeComponent();
            GenerateMain();
            DaysWindow.DaysParent = mainContainer;
            Remainder();
        }
        // remainder
        protected void Remainder() {
            var tommorow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            if (!CurriculumEvent.events.Keys.Contains(tommorow))
                return;
            string title = "Masz jutro wpisane wydarzenie";
            var tommorow_events = CurriculumEvent.events[tommorow];
            string message;
            if (tommorow_events.Count == 1)
                message = "Jutro masz wpisane wydarzenie: " + tommorow_events[0].Name;
            else {
                message = "Jutro masz wpisane wydarzenia (" + tommorow_events.Count.ToString() + "):\n";
                foreach(var e in tommorow_events) {
                    message += "    "+e.Name+"\n";
                }
            }

            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        // saveing 
        protected void ImportEvents() {
            if (!File.Exists("events.json"))
                File.Create("events.json");
            string data = File.ReadAllText("events.json");

            JsonSerializer serializer = new JsonSerializer();
            _ = JsonConvert.DeserializeObject<HashSet<CurriculumEvent>>(File.ReadAllText("events.json")); // constructor saves it

        }
        protected void ExportEvent() {
            HashSet<CurriculumEvent> export = new();
            foreach (var list in CurriculumEvent.events) {
                foreach(CurriculumEvent item in list.Value)
                    export.Add(item);
            }

            string json = JsonConvert.SerializeObject(export, Formatting.Indented);
            using(StreamWriter writer = new("events.json", false)) {
                writer.WriteLine(json);
            }
        }

        // generation
        private void GenerateMain() {
            title.Content = mainDate.ToShortDateString();
            month = new(mainDate);
            CalendarContainer.Child = month;
        }
        // button events
        private void RightButton(object sender, RoutedEventArgs e) {
            mainDate = mainDate.AddMonths(1);
            GenerateMain();
        }
        private void LeftButton(object sender, RoutedEventArgs e) {
            mainDate = mainDate.AddMonths(-1);
            GenerateMain();
        }
        private void Window_Closed(object sender, EventArgs e) {
            ExportEvent();
            Close();
        }
    }
}
