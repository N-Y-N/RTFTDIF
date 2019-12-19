using RTFTDIF.Common.Models;
using RTFTDIF.DataAccesss;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NeotericDev.Commons.Logger.LogManager;

namespace RTFTDIF.Core
{
    public class Service
    {   
        private static Service _service;
        private static SqlCeDB _database;
        
        private Service()
        {
            using (_database = SqlCeDB.NewInstance) ;
            Logger.LogDebug<Service>(this, "Service Instance Created");
        }

        public static Service Instance()
        {
            if (_service == null)
            {
                _service = new Service();
            }

            return _service;
        }

        public List<Category> GetAllCategories() {
            Logger.LogDebug<Service>(this, "Loading All Categories");
            List<Category> categories= _database.GetCategories();
            Logger.LogDebug(this, $"Loaded {categories.Count()} Categories");
            return categories;
        }

        public List<Item> GetCategoryItems(string categoryId)
        {
            Logger.LogDebug(this, $"Getting Items for {categoryId} Category");
            var items = _database.GetItems(categoryId).Where(item => File.Exists(Path.Combine(item.Path, item.Name))).ToList();
            FileInfo fileInfo;
            foreach(var itm in items)
            {
                fileInfo = new FileInfo(Path.Combine(itm.Path, itm.Name));
                itm.Size = ToMB(fileInfo.Length) + "";
                itm.Path = fileInfo.DirectoryName;
                itm.Name = fileInfo.Name;
                itm.Format = fileInfo.Extension;
            }

            Logger.LogDebug(this, $"Loaded {items.Count} items");
            return items;
        }

        public List<string> DeleteCategoryItems(List<string> ids)
        {
            Logger.LogDebug(this, $"Deleting {ids.Count} items");
            List<String> undeletedFiles = new List<string>();
            List<String> deletedFiles = new List<string>();
            var items = _database.GetItems(ids);
            String fullFilePath;
            foreach (var itm in items)
            {
                fullFilePath = Path.Combine(itm.Path, itm.Name);
                try
                {
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                        deletedFiles.Add(itm.Id);
                    }
                }
                catch
                {
                    undeletedFiles.Add(fullFilePath);
                }
            }

            _database.DeleteItems(deletedFiles);

            Logger.LogInfo(this, $"Unable to delete {undeletedFiles.Count} files");
            return undeletedFiles;
        }

        public bool RemoveCategoryItems(List<string> ids)
        {
            Logger.LogDebug(this, $"Removing {ids.Count} items");
            return _database.DeleteItems(ids) == ids.Count;
        }


        public bool AddItems(List<Item> items)
        {
            Logger.LogDebug(this, $"Adding {items.Count} items");
            items.ForEach((item) => item.IsActive = true);
            return _database.SaveItems(items);
        }

        public bool SaveCategory(Category category)
        {
            Logger.LogDebug(this, $"Adding Category : {category.CategoryName}");
            category.Id = Guid.NewGuid().ToString();
            category.IsActive = true;
           return _database.SaveCategory(category);
        }

        private double ToMB(long bytes)
        {
            return DivideByStdBlockSize(bytes);
        }

        private double ToGB(long bytes)
        {
            return DivideByStdBlockSize(ToMB(bytes));
        }

        private double DivideByStdBlockSize(long value)
        {
            return value / 1024;
        }

        private double DivideByStdBlockSize(double value)
        {
            return value / 1024;
        }

        private double DivideByStdBlockSize(int value)
        {
            return value / 1024;
        }
    }
}
