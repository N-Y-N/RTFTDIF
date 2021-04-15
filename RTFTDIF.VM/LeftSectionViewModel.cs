using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RTFTDIF.Common;
using RTFTDIF.Core;
using RTFTDIF.Core.Events;
using RTFTDIF.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using static NeotericDev.Commons.Logger.LogManager;

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
            _eventAggregator.GetEvent<RefreshCategoriesEvent>().Subscribe(ReloadCategories);
            _eventAggregator.GetEvent<UserInputResponseEvent<string>>().Subscribe(CreateCategory);
            LoadAllCategories();
            Logger.LogDebug(this, $"Creating LeftSectionViewModel Instance");
        }

        private void CreateCategory(UserInputResponseEventArgs<string> responseEventArgs)
        {
            if (responseEventArgs.Content != null)
            {
                Category category = new Category();
                category.CategoryName = responseEventArgs.Content;
                category.CreatedDate = DateTime.Now;
                category.UpdatedDate = DateTime.Now;

                _service.SaveCategory(category);
                LoadAllCategories();
            }
        }

        private void ReloadCategories()
        {
            Categories.Clear();
            LoadAllCategories();
        }

        private void LoadAllCategories()
        {
            Logger.LogDebug(this, $"Loading Categories");
            var c = _service.GetAllCategories();
            int totalSize = 0, totalFiles = 0;
            var cc = c.Select(x => new CategoryItemControlViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                FilesCount = x.FilesCount,
                Size = (x.Size != null && !x.Size.Equals("", StringComparison.InvariantCultureIgnoreCase) ? x.Size : "0") + " MB",
                CategoryId = x.Id
            }).ToList();

            foreach(var item in cc)
            {
                totalSize += (item.Size != null) ? Convert.ToInt32(item.Size.Split(' ')[0]) : 0;
                totalFiles += item.FilesCount;
            }

            Total = new CategoryItemControlViewModel()
            {
                FilesCount = totalFiles,
                Size = totalSize + " MB"
            };
            Categories = new ObservableCollection<CategoryItemControlViewModel>(cc);

            Logger.LogDebug(this, $"Loaded {cc.Count} Categories");
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
                            Id = Guid.NewGuid().ToString(),
                            Name = fileInfo.Name,
                            Path = fileInfo.DirectoryName,
                            Size = fileInfo.Length / 1024 / 1024 + "",
                            Type = fileInfo.Attributes.HasFlag(FileAttributes.Directory) ? ItemType.Folder : ItemType.File,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        }
                        ); ; ;
                }
            }
            Logger.LogDebug(this, $"Dragged {items.Count} Items in Category : {draggedFilesEventArgs.CategoryId}");
            _service.AddItems(items);
            _eventAggregator.GetEvent<CategorySelectedEvent>().Publish(draggedFilesEventArgs.CategoryId);
            ReloadCategories();
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
            //var categoriesBackup = Categories;// TODO : Remove if not needed
            Categories = new ObservableCollection<CategoryItemControlViewModel> (Categories.OrderBy(x => sort == Sort.File ? x.FilesCount.ToString() : x.Size.Split(' ')[0]).ToList());
        }

        bool CanExecuteSortCommand(String typeSort)
        {
            return Categories?.Count > 0;
        }

        private DelegateCommand _addCategoryCommand;
        public DelegateCommand AddCategoryCommand =>
            _addCategoryCommand ?? (_addCategoryCommand = new DelegateCommand(ExecuteAddCategoryCommand));

        void ExecuteAddCategoryCommand()
        {
            _eventAggregator.GetEvent<RequestUserInputEvent>().Publish(new RequestUserInputEventArgs() { 
                Title = "Input Required",
                Message = "How do you wanna call"
            });            
        }

        #endregion
    }
}
