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

namespace RTFTDIF.Client.Metro.Views
{
    /// <summary>
    /// Interaction logic for ButtonWithSortIndicator.xaml
    /// </summary>
    public partial class ButtonWithSortIndicator : UserControl
    {
        public ButtonWithSortIndicator()
        {
            InitializeComponent();
        }

        public String Text { get; set; }
    }
}
