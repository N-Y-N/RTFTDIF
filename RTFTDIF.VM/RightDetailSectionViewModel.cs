using Prism.Events;
using Prism.Mvvm;
using RTFTDIF.Core;
using RTFTDIF.Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class RightDetailSectionViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        Service _service;
        #region Constructor
        public RightDetailSectionViewModel(IEventAggregator eventAggregator, Service service)
        {
            _service = service;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CategorySelectedEvent>().Subscribe(LoadCategoryItems);
        }
        #endregion

        private ObservableCollection<ItemDetailViewModel> _items;
        public ObservableCollection<ItemDetailViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }



        private void LoadCategoryItems(string id)
        {
            Items = new ObservableCollection<ItemDetailViewModel>();
            var items = _service.GetCategoryItems(id);
            if (items != null)
            {
                int i = 1;
                items.ForEach(x => Items.Add(new ItemDetailViewModel()
                {
                    Id = x.Id,
                    Format = x.Format,
                    ItemName = x.Name,
                    ItemPath = x.Path,
                    ItemType = x.Type == Common.ItemType.File ? "File" : "Folder",
                    Selected = false,
                    Size = x.Size,
                    SNo = i++
                }));
            }
            System.Diagnostics.Debug.WriteLine($"Event Subsribed and processed at {DateTime.Now}");
        }
    }
}
