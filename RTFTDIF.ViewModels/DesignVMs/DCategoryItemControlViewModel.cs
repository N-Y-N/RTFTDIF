using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    public class DCategoryItemControlViewModel : CategoryItemControlViewModel
    {
        public static DCategoryItemControlViewModel Instance => new DCategoryItemControlViewModel();
        public DCategoryItemControlViewModel()
        {
            CategoryName = "Delete in 1 month";
            FilesCount = 2525;
            Size = "123.75 GB";
        }
    }
}
