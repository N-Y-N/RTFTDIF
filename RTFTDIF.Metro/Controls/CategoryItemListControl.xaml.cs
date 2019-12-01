using Prism.Events;
using Prism.Regions;
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
    /// Interaction logic for CategoryItemListControl.xaml
    /// </summary>
    public partial class CategoryItemListControl : UserControl
    {
        IEventAggregator _eventAggregator;
        public CategoryItemListControl(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            //regionManager.RegisterViewWithRegion("ListRegion", typeof(CategoryItemControl));
            InitializeComponent();
        }

        private void CategoryItemControl_Click(String catId)
        {
            _eventAggregator.GetEvent<CategorySelectedEvent>().Publish(catId);
            System.Diagnostics.Debug.WriteLine($"Event Published at {DateTime.Now}");
        }
    }
}
