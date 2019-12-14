using RTFTDIF.Metro.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Mvvm;
using System;
using MahApps.Metro.Controls;
using RTFTDIF.Core;
using MahApps.Metro.Controls.Dialogs;

namespace RTFTDIF.Metro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Service service = Service.Instance();
            containerRegistry.RegisterInstance<Service>(service);
            containerRegistry.RegisterInstance<IDialogCoordinator>(DialogCoordinator.Instance);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Split(new[] { '.' });
                var viewModelName = $"RTFTDIF.VM.{viewName[viewName.Length - 1]}ViewModel, RTFTDIF.VM";
                return Type.GetType(viewModelName);
            });
        }
    }
}
