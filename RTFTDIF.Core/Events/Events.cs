
using Prism.Events;
using RTFTDIF.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Core.Events
{
    public class DraggedFilesEventArgs {
        public string CategoryId { get; set; }
        public string[] Files { get; set; }
    }

    public class DeleteItemsEventArg {
        public string CategoryId { get; set; }
        public List<string> ItemIds { get; set; }
    }

    public class DraggedFilesEvent : PubSubEvent<DraggedFilesEventArgs>
    {
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

    public class DisplayMessageEvent : PubSubEvent<string>
    {
    }

    public class RefreshCategoriesEvent : PubSubEvent
    {
    }
}
