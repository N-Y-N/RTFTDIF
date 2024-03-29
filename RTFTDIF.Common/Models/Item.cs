﻿using RTFTDIF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Common.Models
{
    public class Item : BaseModel
    {
        public String Name { get; set; }
        public String Path { get; set; }
        public String Size { get; set; }
        public String Format { get; set; }
        public ItemType Type { get; set; }
        public String CategoryId { get; set; }
    }
}
