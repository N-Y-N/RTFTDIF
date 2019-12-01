using Prism.Regions;
using System.Windows.Controls;

namespace RTFTDIF.Metro.Controls
{
    /// <summary>
    /// Interaction logic for CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public CategoryControl(IRegionManager regionManager) 
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("CategoryItemListControlRegion", typeof(CategoryItemListControl));
        }
    }
}
