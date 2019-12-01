using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class CategoryItemControlViewModel : BindableBase
    {
        private string _id;
        private String _categoryName;
        private int _filesCount;
        private String _size;

        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public String Size
        {
            get { return _size; }
            set { SetProperty(ref _size,value); }
        }

        public int FilesCount
        {
            get { return _filesCount; }
            set { SetProperty(ref _filesCount, value); }
        }

        public String CategoryName
        {
            get { return _categoryName; }
            set { SetProperty(ref _categoryName, value); }
        }
    }
}
