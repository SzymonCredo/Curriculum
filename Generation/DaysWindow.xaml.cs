﻿using Curriculum.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
        public static Grid DaysParent;
        Day root;
        public DaysWindow(Day root) {
            this.root = root;
            InitializeComponent();
            
        }

        private void Window_Closed(object sender, EventArgs e) {
            root.WindowClose();
        }
        public void show() {
            this.Visibility = Visibility.Visible;
            if(!DaysParent.Children.Contains(this))
                DaysParent.Children.Add(this);
            { // generate content
                var day = root.date.Day.ToString().Length < 2 ? '0' + root.date.Day.ToString() : root.date.Day.ToString();
                var month = root.date.Month.ToString().Length < 2 ? '0' + root.date.Month.ToString() : root.date.Month.ToString();

                Title.Content = day + "." + month + "." + root.date.Year.ToString();

                // put label for every hour
                for(int i = 0; i < 24; i++) {
                    EventContainer.RowDefinitions.Add(new() { Height = new(50) });
                    var label = new Viewbox() { // label of an hour 
                        Margin = new(2) ,
                        Child = new Label() {
                            Background = Brushes.White ,
                            BorderBrush = Brushes.Black ,
                            BorderThickness = new(1) ,
                            Content = i.ToString() + ":00" ,
                            HorizontalAlignment = HorizontalAlignment.Center
                        }
                    };
                    Grid.SetRow(label , i);
                    EventContainer.Children.Add(label);
                }
                // put all events in place
                var j = 1;
                foreach(CurriculumEvent e in root.events) {
                    var tmp = e.repres;
                    var sth = DateOnly.FromDateTime(e.startTime) != root.date ? 0 : e.startTime.Hour;

                    Grid.SetRow(tmp , sth);
                    
                    var ent = DateOnly.FromDateTime(e.endTime) != root.date ? 23 : e.endTime.Hour;
                    if (ent == 0) ent++;
                    Grid.SetRowSpan(tmp , ent-sth+1);

                    EventContainer.ColumnDefinitions.Add(new());
                    Grid.SetColumn(tmp , j++);
                    EventContainer.Children.Add(tmp);
                }
            }
        }
        public void close() {
            this.Visibility = Visibility.Collapsed;
            if (DaysParent.Children.Contains(this))
                DaysParent.Children.Remove(this);
            // clear events
            {
                EventContainer.Children.Clear();
                while(EventContainer.ColumnDefinitions.Count != 1) {
                    EventContainer.ColumnDefinitions.RemoveAt(1);
                }
                EventContainer.RowDefinitions.Clear();
            }
        }

        private void CreateNewEvent(object sender , RoutedEventArgs e) {
            var window = new EventCreator();
            window.ShowDialog();
        }
    }
}
