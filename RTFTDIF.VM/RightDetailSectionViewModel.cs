using Prism.Commands;
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
            _eventAggregator.GetEvent<FilterItemsEvent>().Subscribe(FilterItems);
        }
        #endregion

        private ObservableCollection<ItemDetailViewModel> _items;
        public ObservableCollection<ItemDetailViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private ObservableCollection<ItemDetailViewModel> _backupData;

        #region Commands
        private DelegateCommand _deleteItemsCommand;
        public DelegateCommand DeleteItemsCommand =>
            _deleteItemsCommand ?? (_deleteItemsCommand = new DelegateCommand(ExecuteDeleteItemsCommand, CanExecuteDeleteItemsCommand).ObservesProperty(()=> Items));

        void ExecuteDeleteItemsCommand()
        {
           PopulatData( _service.DeleteCategoryItems(Items.Where(x=>x.Selected).Select(x=>x.Id).ToList()));
        }

        bool CanExecuteDeleteItemsCommand()
        {
            return Items != null && Items.Count() > 0;
        }

        private DelegateCommand removeItemsCommand;
        public DelegateCommand RemoveItemsCommand =>
            removeItemsCommand ?? (removeItemsCommand = new DelegateCommand(ExecuteRemoveItemsCommand, CanExecuteRemoveItemsCommand).ObservesProperty(() => Items));

        void ExecuteRemoveItemsCommand()
        {
            PopulatData(_service.RemoveCategoryItems(Items.Where(x => x.Selected).Select(x => x.Id).ToList()));
        }

        bool CanExecuteRemoveItemsCommand()
        {
            return Items != null && Items.Count() > 0;
        }
        #endregion

        private void LoadCategoryItems(string id)
        {
            var ii = _service.GetCategoryItems(id);
            List<ItemDetailViewModel> items = PopulatData(ii);
            

            System.Diagnostics.Debug.WriteLine($"Event Subsribed and processed at {DateTime.Now}");
        }

        private List<ItemDetailViewModel> PopulatData( List<Core.Models.Item> ii)
        {
            int i = 1;
            var items = ii.Select(x => new ItemDetailViewModel()
            {
                Id = x.Id,
                Format = x.Format,
                ItemName = x.Name,
                ItemPath = x.Path,
                ItemType = x.Type == Common.ItemType.File ? "File" : "Folder",
                Selected = true,
                Size = x.Size,
                SNo = i++
            }).ToList();

            Items = new ObservableCollection<ItemDetailViewModel>(items);
            _backupData = new ObservableCollection<ItemDetailViewModel>(items);
            return items;
        }

        private void FilterItems(String filterText)
        {
            Items.Clear();
            List<ItemDetailViewModel> filteredItems;
            if (String.IsNullOrEmpty(filterText))
            {
                filteredItems = _backupData.Select(x => x).ToList();
            }
            else
            {
                if (filterText.Contains("*.")) filterText = filterText.Substring(2, filterText.Length - 2);
                filteredItems = _backupData.Where(x => x.ItemName.ToLower().Contains(filterText.ToLower()) ||
                                x.ItemPath.ToLower().Contains(filterText.ToLower()) ||
                                x.Format.ToLower().Contains(filterText.ToLower()) ||
                                x.ItemType.ToLower().Contains(filterText.ToLower())).ToList();
            }
            Items = new ObservableCollection<ItemDetailViewModel>(filteredItems);
        }

    }
}
