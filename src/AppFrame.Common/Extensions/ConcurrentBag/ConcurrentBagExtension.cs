using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
 
namespace AppFrame.Common.Extensions.ConcurrentBag
{
    public static class ConcurrentBagExtension
    {
        public static void Remove<T>(this ConcurrentBag<T> bag,T model)  //  并发集合中移除指定元素
        {
            if (bag == null) return;
            if (!bag.Contains(model)) return;
            while (bag.Count > 0)
            {
                bag.TryTake(out T result);

                if (result.Equals(model))
                {
                    break;
                }

                bag.Add(result);
            }
        }
    }
}
