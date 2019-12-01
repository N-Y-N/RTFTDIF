
using Prism.Events;
using RTFTDIF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Core.Events
{
    public class CategorySelectedEvent : PubSubEvent<string>
    {
    }

    public class CategoryItemsLoadedEvent : PubSubEvent<List<Item>>
    { 
    }
}
