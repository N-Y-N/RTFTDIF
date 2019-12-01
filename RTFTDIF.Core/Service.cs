using RTFTDIF.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Core
{
    public class Service
    {
        private static List<CategoryItemModel> _categories = new List<CategoryItemModel>();
        private static Service _service;

        private Service()
        {
            Random rnd = new Random();
            for (int i = 1; i <= 2000; i++)
            {
                _categories.Add(new CategoryItemModel()
                {
                    Id = "C" + i,
                    CategoryName = $"Category - {i}",
                    FilesCount = rnd.Next(1, 100),
                    Size = $"{rnd.Next(50, 5000)} MB",
                }); ;
            }
        }

        public static Service Instance()
        {
            if (_service == null)
            {
                _service = new Service();
            }
            return _service;
        }

        public List<CategoryItemModel> GetAllCategories() {
            return  _categories;
        }

        public List<Item> GetCategoryItems(string id)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Debug.WriteLine("-------------> Get Items Started : " + id);
            Debug.WriteLine(DateTime.Now);
            List<Item> items = new List<Item>();
            Random rnd = new Random();
            for (int i = 1; i <= 100; i++) {
                items.Add(new Item() { 
                    Id = i + "",
                    Name = id + "_Item " + i,
                    Path = "F:/Dummy/Path/To/File/",
                    Size = rnd.Next(1,50) + "MB",
                    Format = "mp3"
                });;
            }
            sw.Stop();
            Debug.WriteLine("-------------> Get Items Ended : " + id);
            Debug.WriteLine("Total MilliSeconds : " + sw.ElapsedMilliseconds);
            Debug.WriteLine(DateTime.Now);
            return items;
        }

        
    }
}
