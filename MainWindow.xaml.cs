using Curriculum.Events;
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

        DateOnly mainDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
        List<Week> weeks = new List<Week>();
        public MainWindow() {
            ImportEvents();
            InitializeComponent();
            MainGenerate();
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
        private void MainGenerate() {
            DaysWindow.DaysParent = mainContainer;
            title.Content = mainDate.Year.ToString() + " " + Months[mainDate.Month - 1];
            main.RowDefinitions.Add(new());
            string[] days = { "Poniedzi", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
            for (int i = 0; i < days.Length; i++) {
                var tmp = new Label() {
                    Content = days[i]
                };
                Grid.SetColumn(tmp, i + 1);
                Grid.SetRow(tmp, 0);
                main.Children.Add(tmp);
            }

            int nr = 0;
            int MainDayOfWeek = mainDate.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)mainDate.DayOfWeek -1;
            DateOnly j = mainDate.AddDays(- MainDayOfWeek% 7 + 1);
            do {
                main.RowDefinitions.Add(new());
                Week tmp = new(j, nr++, main);
                weeks.Add(tmp);
                j = j.AddDays(7);
            }
            while (j.Month == mainDate.Month);
        }
        private void MainClear() {
            main.Children.Clear();
            main.RowDefinitions.Clear();
        
        }
        private static string[] Months = {"Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"};

        // UI events
        private void RightButton(object sender, RoutedEventArgs e) {
            MainClear();
            mainDate = mainDate.AddMonths(1);
            MainGenerate();
        }
        private void LeftButton(object sender, RoutedEventArgs e) {
            MainClear();
            mainDate = mainDate.AddMonths(-1);
            MainGenerate();
        }

        private void Window_Closed(object sender, EventArgs e) {
            ExportEvent();
            Close();
        }
    }
}
