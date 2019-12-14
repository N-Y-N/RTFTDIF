using Prism.Mvvm;
using RTFTDIF.Core;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
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

        private int _minWidth;
        public int MinWidth
        {
            get { return _minWidth; }
            set { SetProperty(ref _minWidth, value); }
        }

        private int _minheight;
        public int MinHeight
        {
            get { return _minheight; }
            set { SetProperty(ref _minheight, value); }
        }


        private Service _service = Service.Instance();

        public MainWindowViewModel()
        {
            Title = "RTFTDIF-v0.1";
            MinWidth = Convert.ToInt32(ConfigurationManager.AppSettings["WinMinWidth"]);
            MinHeight =Convert.ToInt32(ConfigurationManager.AppSettings["WinMinHeight"]);
            
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
