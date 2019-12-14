using Prism.Events;
using Prism.Regions;
using RTFTDIF.Core.Events;
using System.Windows;
using System.Windows.Controls;

namespace RTFTDIF.Metro.Controls
{
    /// <summary>
    /// Interaction logic for CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        IEventAggregator _eventAggregator;
        public CategoryControl(IRegionManager regionManager, IEventAggregator ea) 
        {
            _eventAggregator = ea;
            InitializeComponent();
            
        }

        private void CategoryItemControl_Click(string categoryId)
        {
            _eventAggregator.GetEvent<CategorySelectedEvent>().Publish(categoryId);
        }

        private void CategoryItemControl_Drop(object sender, DragEventArgs e)
        {
            string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            DraggedFilesEventArgs args = new DraggedFilesEventArgs()
            {
                Files = draggedFiles,
                CategoryId = ((CategoryItemControl)sender).CatId
            };
            _eventAggregator.GetEvent<DraggedFilesEvent>().Publish(args);
        }
    }
}
