using RTFTDIF.Common.Models;
using RTFTDIF.DataAccesss;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Core
{
    public class Service
    {
        private static List<Category> _categories = new List<Category>();
        List<Item> items = new List<Item>();
        private static Service _service;
        private static SqlCeDB _database;
        
        private Service()
        {
            using (_database = SqlCeDB.NewInstance);
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
            return _database.GetCategories();
        }

        public List<Item> GetCategoryItems(string categoryId)
        {
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

            return items.ToList();
        }

        public List<string> DeleteCategoryItems(List<string> ids)
        {
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
            return undeletedFiles;
        }

        public bool RemoveCategoryItems(List<string> ids)
        {
            return _database.DeleteItems(ids) == ids.Count;
        }


        public bool AddItems(List<Item> items)
        {
            items.ForEach((item) => item.IsActive = true);
            return _database.SaveItems(items);
        }

        public bool SaveCategory(Category category) 
        {
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
