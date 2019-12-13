using Prism.Mvvm;
using RTFTDIF.Core;
using System.Collections.ObjectModel;
using System.Linq;

namespace RTFTDIF.VM
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        
        private Service _service = Service.Instance();

        public MainWindowViewModel()
        {
            Title = "RTFTDIF-v0.1";
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
    }
}
