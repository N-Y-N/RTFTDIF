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
        List<Item> items = new List<Item>();
        private static Service _service;

        private static Dictionary<CategoryItemModel, List<Item>> data = new Dictionary<CategoryItemModel, List<Item>>();

        private Service()
        {
            Random rnd = new Random();
            for (int i = 1; i <= 100; i++)
            {
                var cat = new CategoryItemModel()
                {
                    Id = "C" + i,
                    CategoryName = $"Category - {i}",
                    FilesCount = rnd.Next(1, 100),
                    Size = $"{rnd.Next(50, 5000)} MB",
                };
                var itms = new List<Item>();
                for (int j = 1; j <= 1000; j++)
                {
                    itms.Add(new Item()
                    {
                        Id = "I" + i + "" + j,
                        Name = $"C{i}_Item " + j,
                        Path = "F:/Dummy/Path/To/File/",
                        Size = rnd.Next(1, 50) + "MB",
                        Format = "mp3"
                    }); ;
                }
                data.Add(cat, itms);
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
            return  data.Keys.ToList();
        }

        public List<Item> GetCategoryItems(string id)
        {
            var key = data.Keys.First(x => x.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            List<Item> i = new List<Item>();
            data.TryGetValue(key, out i);
            return i;
        }

        public List<Item> DeleteCategoryItems(string catId, List<string> ids)
        {
            var key = data.Keys.First(x => x.Id.Equals(catId, StringComparison.InvariantCultureIgnoreCase));
            List<Item> i = new List<Item>();
            data.TryGetValue(key, out i);
            i = i.Where(x => !ids.Contains(x.Id)).ToList();
            
            return i;
        }

        public List<Item> DeleteCategoryItems(List<string> ids)
        {
            List<Item> d = new List<Item>();
            foreach (var k in data.Keys)
            {                
                if (data.TryGetValue(k, out d))
                {
                    bool contains = d.Any(it => ids.Contains(it.Id));
                    if (contains)
                    { 
                        d = d.Where(x => !ids.Contains(x.Id)).ToList();
                        data.Remove(k);
                        data.Add(k, d);
                        break;
                    }
                }
            }

            return d;
        }

        public List<Item> RemoveCategoryItems(List<string> ids)
        {
            List<Item> d = new List<Item>();
            foreach (var k in data.Keys)
            {
                if (data.TryGetValue(k, out d))
                {
                    bool contains = d.Any(it => ids.Contains(it.Id));
                    if (contains)
                    {
                        d = d.Where(x => !ids.Contains(x.Id)).ToList();
                        data.Remove(k);
                        data.Add(k, d);
                        break;
                    }
                }
            }

            return d;
        }


    }
}
