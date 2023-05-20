using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.Backend.Core.Extensions;

public static class ObjectExtensions
{
    public static List<T> AsList<T>(this T obj)
    {
        if (obj is List<T> list)
        {
            return list;
        }
        else
        {
            return new List<T> { obj };
        }
    }
}
