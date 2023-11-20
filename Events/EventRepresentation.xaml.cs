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

namespace Curriculum.Events {
    /// <summary>
    /// Interaction logic for EventRepresentation.xaml
    /// </summary>
    public partial class EventRepresentation : UserControl {
        private CurriculumEvent root;
        public EventRepresentation(CurriculumEvent root) {
            this.root = root;
            InitializeComponent();
            title.Content = root.Name;
            desc.Text = root.Description;

        }

        private void Button_Click(object sender , RoutedEventArgs e) {
            foreach(var key in CurriculumEvent.events.Keys) {
                if(CurriculumEvent.events[key].IndexOf(root) != -1)                    
                    CurriculumEvent.events[key].Remove(root);
            }
            this.Visibility = Visibility.Hidden;
            
        }
    }
}
