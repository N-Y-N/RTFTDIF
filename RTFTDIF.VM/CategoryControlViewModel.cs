using Prism.Commands;
using Prism.Mvvm;
using RTFTDIF.Common;
using RTFTDIF.Core;
using RTFTDIF.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.VM
{
    public class CategoryControlViewModel : BindableBase
    {
        private Service _service = Service.Instance();
        
        public CategoryControlViewModel()
        {
           
        }
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
    }
}
