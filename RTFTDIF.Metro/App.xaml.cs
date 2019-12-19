using RTFTDIF.Metro.Views;
using Prism.Ioc;
using System.Windows;
using Prism.Mvvm;
using System;
using RTFTDIF.Core;
using MahApps.Metro.Controls.Dialogs;
using static NeotericDev.Commons.Logger.LogManager;
using NeotericDev.Commons.Logger;

namespace RTFTDIF.Metro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            LogManager.InitializeConfig();
            Logger.LogDebug(this, $"App initializing");
        }
        protected override Window CreateShell()
        {
            Logger.LogDebug(this, $"Creating Shell");
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Service service = Service.Instance();
            containerRegistry.RegisterInstance<Service>(service);
            containerRegistry.RegisterInstance<IDialogCoordinator>(DialogCoordinator.Instance);
            Logger.LogDebug(this, $"Registering Types");
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Split(new[] { '.' });
                var viewModelName = $"RTFTDIF.VM.{viewName[viewName.Length - 1]}ViewModel, RTFTDIF.VM";
                Logger.LogDebug(this, $"Resolving ViewModel");
                return Type.GetType(viewModelName);
            });
        }
    }
}
