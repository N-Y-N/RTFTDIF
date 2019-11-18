
using System.IO;
using System.Linq;
using System.Reflection;
using RTFTDIF.ViewModels;

namespace RTFTDIF.Client.Metro {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;

    public class AppBootstrapper : BootstrapperBase {
        SimpleContainer container;

        public AppBootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, ShellViewModel>();

            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "RTFTDIF.Client.Metro.Views",
                DefaultSubNamespaceForViewModels = "RTFTDIF.ViewModels"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies.AddRange(base.SelectAssemblies());
            string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory());
            assemblies.AddRange(from fileName in fileEntries
                where fileName.Contains("RTFTDIF.ViewModels.dll")
                select Assembly.LoadFile(fileName));
            return assemblies;
        }
    }
}