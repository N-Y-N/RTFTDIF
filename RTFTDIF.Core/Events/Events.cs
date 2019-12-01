﻿
using Prism.Events;
using RTFTDIF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Core.Events
{
    public class DeleteItemsEventArg {
        public string CategoryId { get; set; }
        public List<string> ItemIds { get; set; }
    }

    public class DeleteItemsEvent : PubSubEvent<DeleteItemsEventArg>
    { 
    }

    public class FilterItemsEvent : PubSubEvent<String> 
    {
    }

    public class CategorySelectedEvent : PubSubEvent<string>
    {
    }

    public class CategoryItemsLoadedEvent : PubSubEvent<List<Item>>
    { 
    }
}
