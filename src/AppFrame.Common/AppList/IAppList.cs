using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppList
{
    public interface IAppList<T>: ICollection<T>, IEnumerable<T>, IEnumerable where T : class
    {       
        T this[int index]
        {            
            get;
        }
        T this[string name]
        {
            get;
        }
        int IndexOf(T item);
        void Insert(int index, T item);
        void RemoveAt(int index);
    }
}
