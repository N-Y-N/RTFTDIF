using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    public class CategoryItemListViewModel : BaseViewModel
    {
        private List<CategoryItemViewModel> _list;

        public List<CategoryItemViewModel> List
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}
