using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    public class DDetailViewModel : DetailViewModel 
    {
        public static DDetailViewModel Instance => new DDetailViewModel();

        public DDetailViewModel()
        {
            Items = new List<ItemViewModel>();
            Random r = new Random();
            for (int i = 1; i <= 100; i++)
            {
                Items.Add(
                    new DItemViewModel()
                    {
                        SNo = i,
                        File = $"File00000000000 - {i}",
                        Path = i % 10 == 0 ? "D:/Dummy/Path/To/This/File/Is/Little/Soooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo/Long" : "D:/Dummy/Path/To/This/File",
                        Type = ".mp3",
                        Size = r.Next() + " MB"
                    }
                    );
            }
        }
    }
}
