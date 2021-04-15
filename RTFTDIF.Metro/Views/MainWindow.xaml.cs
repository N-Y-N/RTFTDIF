using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Events;
using Prism.Regions;
using RTFTDIF.Core.Events;
using RTFTDIF.Metro.Controls;
using RTFTDIF.VM;
using System;
using System.Windows;
using static NeotericDev.Commons.Logger.LogManager;

namespace RTFTDIF.Metro.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IRegionManager regionManager;
        private IEventAggregator _eventAggregator;
        public MainWindow(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<DisplayMessageEvent>().Subscribe(DisplayMessage);
            eventAggregator.GetEvent<RequestUserInputEvent>().Subscribe(RequestUserInput);
            RegisterRegions();
            Logger.LogInfo(this, $"Creating MainWindows");
        }

        private async void RequestUserInput(RequestUserInputEventArgs reqUserInputArg)
        {
            Logger.LogDebug(this, $"User Input Requested");
            MetroDialogSettings dialogSettings = new MetroDialogSettings() { 
                AnimateHide = false,
                AnimateShow = false
            };
            string responseContent = await this.ShowInputAsync(reqUserInputArg.Title, reqUserInputArg.Message, dialogSettings);
            _eventAggregator.GetEvent<UserInputResponseEvent<string>>().Publish(
                new UserInputResponseEventArgs<string>() { 
                    Content = responseContent
                });
        }

        private void DisplayMessage(string message)
        {
            Logger.LogDebug(this, $"Displaying Message");
            this.ShowMessageAsync("Attention Please!",message);
        }

        private void RegisterRegions()
        {
            Logger.LogDebug(this, $"Registering Regions");
            regionManager.RegisterViewWithRegion("LeftPanRegion", typeof(LeftSection));
            regionManager.RegisterViewWithRegion("RightPanRegion", typeof(RightDetailSection));
        }
    }
}
