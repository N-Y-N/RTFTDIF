using Caliburn.Micro;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel : Screen
    {
        public String TextBlock { get; set; } = "Test";
    }
}
