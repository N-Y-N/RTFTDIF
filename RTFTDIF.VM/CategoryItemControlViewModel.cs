using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RTFTDIF.Common;
using RTFTDIF.Core;
using RTFTDIF.Core.Events;
using RTFTDIF.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class CategoryItemControlViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private string _id;
        private String _categoryName;
        private int _filesCount;
        private String _size;
        private bool isDragEnter;
        private string _categoryId;
        
        private Service _service;

        public CategoryItemControlViewModel()
        {

        }

        public CategoryItemControlViewModel(IEventAggregator eventAggregator, Service service)
        {
            _service = service;
            _eventAggregator = eventAggregator;
            
        }

        public bool IsDragEnter
        {
            get { return isDragEnter; }
            set { SetProperty(ref isDragEnter, value); }
        }
        

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

        public string CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private DelegateCommand _flipDragEnter;
        public DelegateCommand FlipDragEnter =>
            _flipDragEnter ?? (_flipDragEnter = new DelegateCommand(ExecuteFlipDragEnter));

        void ExecuteFlipDragEnter()
        {
            IsDragEnter = !IsDragEnter;
        }

        
    }
}
