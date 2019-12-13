using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RTFTDIF.Common;
using RTFTDIF.Core;
using RTFTDIF.Core.Events;
using RTFTDIF.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class LeftSectionViewModel : BindableBase
    {
        private Service _service;
        private IEventAggregator _eventAggregator;
        #region Constructor
        public LeftSectionViewModel(Service service, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _service = service;
            _eventAggregator.GetEvent<DraggedFilesEvent>().Subscribe(OnDraggedFiles);

            var c = _service.GetAllCategories();
            var cc = c.Select(x => new CategoryItemControlViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                FilesCount = x.FilesCount,
                Size = x.Size,
                CategoryId = x.Id
            }).ToList();
            Total = new CategoryItemControlViewModel()
            {
                FilesCount = 1000,
                Size = "56 GB"
            };
            Categories = new ObservableCollection<CategoryItemControlViewModel>(cc);
        }

        private void OnDraggedFiles(DraggedFilesEventArgs draggedFilesEventArgs)
        {
            var items = new List<Item>();
            Random rnd = new Random();
            foreach (string file in draggedFilesEventArgs.Files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Attributes != FileAttributes.Directory && fileInfo.Exists)
                {
                    items.Add(
                        new Item()
                        {
                            CategoryId = draggedFilesEventArgs.CategoryId,
                            Format = fileInfo.Extension,
                            Id = "D_I" + rnd.Next(1, 1000),
                            Name = fileInfo.Name,
                            Path = fileInfo.DirectoryName,
                            Size = fileInfo.Length / 1024 / 1024 + "MB",
                            Type = fileInfo.Attributes.HasFlag(FileAttributes.Directory) ? ItemType.Folder : ItemType.File
                        }
                        ); ; ;
                }
            }
            _service.AddItems(items);
        }

        #endregion

        #region Properties

        private ObservableCollection<CategoryItemControlViewModel> categories;

        public ObservableCollection<CategoryItemControlViewModel> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        private CategoryItemControlViewModel total;

        public CategoryItemControlViewModel Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }
        #endregion


        #region Commands
        private DelegateCommand<String> _sortCommand;
        public DelegateCommand<String> SortCommand =>
            _sortCommand ?? (_sortCommand = new DelegateCommand<String>(ExecuteSortCommand, CanExecuteSortCommand));

        void ExecuteSortCommand(String typeSort)
        {
            Sort sort = EnumParser.Parse<Sort>(typeSort);
            //var categoriesBackup = Categories; TODO : Remove if not needed
            Categories = new ObservableCollection<CategoryItemControlViewModel> (Categories.OrderBy(x => sort == Sort.File ? x.FilesCount.ToString() : x.Size).ToList());
        }

        bool CanExecuteSortCommand(String typeSort)
        {
            return Categories?.Count > 0;
        }

        private DelegateCommand<string> _getItemsCommand;
        public DelegateCommand<string> GetItemsCommand =>
            _getItemsCommand ?? (_getItemsCommand = new DelegateCommand<string>(ExecuteGetItemsCommand));

        void ExecuteGetItemsCommand(string parameter)
        {
            
        }

        bool CanExecuteGetItemsCommand(String id) {
            return true;
        }
        #endregion
    }
}
