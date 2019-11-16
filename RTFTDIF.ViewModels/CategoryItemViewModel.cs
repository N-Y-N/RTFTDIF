using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    public class CategoryItemViewModel : BaseViewModel
    {
        private String _categoryName;
        private int _filesCount;
        private String _size;

        public String Size
        {
            get { return _size; }
            set { _size = value; }
        }
        
        public int FilesCount
        {
            get { return _filesCount; }
            set { _filesCount = value; }
        }

        public String CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

    }
}
