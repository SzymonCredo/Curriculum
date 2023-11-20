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
            }
            else
                hexView.BorderBrush = Brushes.Red;
        }
        private void Button_Click(object sender , RoutedEventArgs e) {

        }

        
    }
}
