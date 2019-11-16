using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    public class DCategoryItemViewModel : CategoryItemViewModel
    {
        public static DCategoryItemViewModel Instance => new DCategoryItemViewModel();

        public DCategoryItemViewModel()
        {
            CategoryName = "Delete in 1 month";
            FilesCount = 2525;
            Size = "123.75 GB";
        }
    }
}
