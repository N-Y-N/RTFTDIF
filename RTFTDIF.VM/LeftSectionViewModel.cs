using Prism.Commands;
using Prism.Mvvm;
using RTFTDIF.Common;
using RTFTDIF.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class LeftSectionViewModel : BindableBase
    {
        private Service _service;

        #region Constructor
        public LeftSectionViewModel(Service service)
        {
            _service = service;
            var c = _service.GetAllCategories();
            var cc = c.Select(x => new CategoryItemControlViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                FilesCount = x.FilesCount,
                Size = x.Size
            }).ToList();
            Total = new CategoryItemControlViewModel()
            {
                FilesCount = 1000,
                Size = "56 GB"
            };
            Categories = new ObservableCollection<CategoryItemControlViewModel>(cc);
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
            Categories.OrderBy(x => sort == Sort.File ? x.FilesCount.ToString() : x.Size);
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
