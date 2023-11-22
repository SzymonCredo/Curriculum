using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Curriculum.Events {
    /// <summary>
    /// Interaction logic for EventCreator.xaml
    /// </summary>
    public partial class EventCreator : Window {
        public EventCreator() {
            InitializeComponent();
        }
        /*
         private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
private static bool IsTextAllowed(string text)
{
    return !_regex.IsMatch(text);
}
         */
        
        private void HexValidation(object sender , TextCompositionEventArgs e) {
            var s = sender as TextBox;
            Regex r = new("[A-f]|[0-9]");
            if(s.Text.Length == 0 && e.Text != "#")
                e.Handled = true;
            if(s.Text.Length != 0 && !r.IsMatch(e.Text))
                e.Handled = true;
            if (s.Text.Length >= 7)
                e.Handled = true;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            var s = sender as TextBox;
            var hex = s.Text;
            
            if (s.Text.Length == 4)
                hex += s.Text.Substring(1, 3);

            if (hexView == null)
                return;
            if (hex.Length == 7) {
                hexView.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
                hexView.BorderBrush = Brushes.Black;
                s.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
            }
            else
                hexView.BorderBrush = Brushes.Red;
        }
        public CurriculumEvent? resoult { get; private set; } = null;
        private void Button_Click(object sender , RoutedEventArgs e) {
            bool valid = true;
            if (name.Text == ""){
                valid = false;
                name.BorderBrush = Brushes.Red;
            }
            if (desc.Text == "") {
                valid = false;
                desc.BorderBrush = Brushes.Red;
            }
            if(startDate.Value == null || endDate.Value < startDate.Value) {
                startDate.BorderBrush = Brushes.Red;
                valid = false;
            }
            else
                startDate.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
            if (endDate.Value == null || endDate.Value < startDate.Value) {
                endDate.BorderBrush = Brushes.Red;
                valid = false;
            }
            else
                endDate.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
            var hex = hexInput.Text;
            if (hex.Length == 4) hex += hex.Substring(1);
            if (hex.Length < 7) {
                hexInput.BorderBrush = Brushes.Red;
                valid = false;
            }
            if (!valid)
                return;
            resoult = new(name.Text,  (DateTime)startDate.Value, (DateTime)endDate.Value, desc.Text, hex);
            Close();

        }

        private void StrValidate(object sender, TextChangedEventArgs e) {
            var s = sender as TextBox;
            if(s.Text == "")
                s.BorderBrush = Brushes.Red;
            else
                s.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3"));
        }
    }
}
