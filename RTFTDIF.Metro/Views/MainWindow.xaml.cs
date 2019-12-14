using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Regions;
using RTFTDIF.Core.Events;
using RTFTDIF.Metro.Controls;
using RTFTDIF.VM;
using System;
using System.Windows;

namespace RTFTDIF.Metro.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IRegionManager regionManager;
        public MainWindow(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            eventAggregator.GetEvent<DisplayMessageEvent>().Subscribe(DisplayMessage);
            RegisterRegions();
        }

        private void DisplayMessage(string message)
        {
            this.ShowMessageAsync("Attention Please!",message);
        }

        private void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("LeftPanRegion", typeof(LeftSection));
            regionManager.RegisterViewWithRegion("RightPanRegion", typeof(RightDetailSection));
           // DataContext = new LeftSectionViewModel();
        }
    }
}
