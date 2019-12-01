using Prism.Events;
using RTFTDIF.Core;
using RTFTDIF.Core.Events;
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

namespace RTFTDIF.Metro.Controls
{
    /// <summary>
    /// Interaction logic for RightDetailSection.xaml
    /// </summary>
    public partial class RightDetailSection : UserControl
    {
        IEventAggregator _eventAggregator;
        public RightDetailSection(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
        }

        private void GridScroller_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null) {
                _eventAggregator.GetEvent<FilterItemsEvent>().Publish(textBox.Text);
            }
            
        }
    }
}
