using Caliburn.Micro;
using PropertyChanged;

namespace RTFTDIF.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ShellViewModel : Conductor<object>, IShell {
        public ShellViewModel()
        {
            ActivateItem(new HomeViewModel());
            WindowTitle = "RTFTDIF-v0.1";
        }

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }

    }
}