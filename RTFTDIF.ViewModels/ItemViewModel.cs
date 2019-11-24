using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.ViewModels
{
    public class ItemViewModel
    {
        public bool IsSelected { get; set; }
        public int SNo { get; set; }
        public String File { get; set; }
        public String Path { get; set; }
        public String Type { get; set; }
        public String Size { get; set; }    
    }
}
