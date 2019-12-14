using Prism.Regions;
using System.Windows.Controls;

namespace RTFTDIF.Metro.Controls
{
    /// <summary>
    /// Interaction logic for LeftSection.xaml
    /// </summary>
    public partial class LeftSection : UserControl
    {
        public LeftSection(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("CategoryRegion", typeof(CategoryControl));            
        }
    }
}
