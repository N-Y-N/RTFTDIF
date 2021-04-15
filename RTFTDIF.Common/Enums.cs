using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTFTDIF.Common
{
    public enum Sort
    {
        File, Size
    }

    public enum ItemType
    {
        File, Folder
    }

    public class EnumParser
    {

        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
