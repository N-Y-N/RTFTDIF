using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Common.Models
{
    public class Category : BaseModel
    {
        public String Size { get; set; }
        public int FilesCount { get; set; }
        public String CategoryName { get; set; }
    }
}
