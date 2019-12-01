using MahApps.Metro.Controls;
using Prism.Regions;
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
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            RegisterRegions();
        }

        private void RegisterRegions()
        {
            regionManager.RegisterViewWithRegion("LeftPanRegion", typeof(LeftSection));
            regionManager.RegisterViewWithRegion("RightPanRegion", typeof(RightDetailSection));
           // DataContext = new LeftSectionViewModel();
        }
    }
}
