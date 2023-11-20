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
            Regex r = new("([^0-9]|[^a-F])+");
            if(!r.IsMatch(e.Text))
                e.Handled = true;
            e.Text.Replace('f' , 'F');
            if(e.Text.Length == 6)
                hexView.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(e.Text));
        }

        private void Button_Click(object sender , RoutedEventArgs e) {

        }

    }
}
