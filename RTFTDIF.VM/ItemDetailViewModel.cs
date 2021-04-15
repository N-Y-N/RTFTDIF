using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class ItemDetailViewModel : BindableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private int _sNo;
        public int SNo
        {
            get { return _sNo; }
            set { SetProperty(ref _sNo, value); }
        }
        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }
        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set { SetProperty(ref _itemName, value); }
        }
        private string _itemPath;
        public string ItemPath
        {
            get { return _itemPath; }
            set { SetProperty(ref _itemPath, value); }
        }
        private String _itemType;
        public String ItemType
        {
            get { return _itemType; }
            set { SetProperty(ref _itemType, value); }
        }
        private string _size;
        public string Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }
        private string _format;
        public string Format
        {
            get { return _format; }
            set { SetProperty(ref _format, value); }
        }
    }
}
