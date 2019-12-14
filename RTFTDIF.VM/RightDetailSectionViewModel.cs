using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RTFTDIF.Common.Models;
using RTFTDIF.Core;
using RTFTDIF.Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

        #region Properties

        private ObservableCollection<ItemDetailViewModel> _backupData;

        private ObservableCollection<ItemDetailViewModel> _items;
        public ObservableCollection<ItemDetailViewModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private string _categoryId;
        public string CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private ItemDetailViewModel _selectedItem;
        public ItemDetailViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        #endregion

        #region Commands
        private DelegateCommand<string> _deleteItemsCommand;
        public DelegateCommand<string> DeleteItemsCommand =>
            _deleteItemsCommand ?? (_deleteItemsCommand = new DelegateCommand<string>(ExecuteDeleteItemsCommand, CanExecuteDeleteItemsCommand).ObservesProperty(()=> Items));

        void ExecuteDeleteItemsCommand(string categoryId)
        {           
            var undeletedFiles = _service.DeleteCategoryItems(Items.Where(x => x.Selected).Select(x => x.Id).ToList());
            if(undeletedFiles != null && undeletedFiles.Count > 0)
            {
                int fileCount = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append("\nUnable to delete the following files");
                sb.Append("\n====================================");
                foreach (var file in undeletedFiles)
                {
                    sb.Append($"\n{++fileCount}. {file}");
                }
                _eventAggregator.GetEvent<DisplayMessageEvent>().Publish(sb.ToString());
            }
            _eventAggregator.GetEvent<RefreshCategoriesEvent>().Publish();
            PopulatData(_service.GetCategoryItems(categoryId));
        }

        bool CanExecuteDeleteItemsCommand(string categoryId)
        {
            return Items != null && Items.Count() > 0;
        }

        private DelegateCommand<string> removeItemsCommand;
        public DelegateCommand<string> RemoveItemsCommand =>
            removeItemsCommand ?? (removeItemsCommand = new DelegateCommand<string>(ExecuteRemoveItemsCommand, CanExecuteRemoveItemsCommand).ObservesProperty(() => Items));

        void ExecuteRemoveItemsCommand(string categoryId)
        {
            _service.RemoveCategoryItems(Items.Where(x => x.Selected).Select(x => x.Id).ToList());
            PopulatData(_service.GetCategoryItems(categoryId));
        }

        bool CanExecuteRemoveItemsCommand(string categoryId)
        {
            return Items != null && Items.Count() > 0;
        }

        private DelegateCommand _openInExplorerCommand;
        public DelegateCommand OpenInExplorerCommand =>
            _openInExplorerCommand ?? (_openInExplorerCommand = new DelegateCommand(ExecuteOpenInExplorerCommand, CanExecuteOpenInExplorerCommand).ObservesProperty(()=> SelectedItem));

        void ExecuteOpenInExplorerCommand()
        {
            string arg = "/select, \"" + Path.Combine(SelectedItem.ItemPath, SelectedItem.ItemName) + "\"";
            Process explorer = Process.Start("explorer.exe", arg);

            System.Threading.Thread.Sleep(3000);
        }

        bool CanExecuteOpenInExplorerCommand()
        {
            return SelectedItem != null;
        }

        #endregion

        private void LoadCategoryItems(string id)
        {
            CategoryId = id;
            var categoryItems = _service.GetCategoryItems(id);
            List<ItemDetailViewModel> items = PopulatData(categoryItems);
            

            System.Diagnostics.Debug.WriteLine($"Event Subsribed and processed at {DateTime.Now}");
        }

        private List<ItemDetailViewModel> PopulatData( List<Item> ii)
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
