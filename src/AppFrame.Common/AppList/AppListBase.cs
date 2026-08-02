using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppList
{
    public abstract class AppListBase<T> : IAppList<T> where T : class
    {
        protected readonly static object _locker = new object();
        protected readonly List<T> _items = new List<T>();

        public virtual T this[int index] => (index >= 0 && index < Count) ? _items[index] : null;
        public virtual T this[string name] => null;

        public virtual int Count => _items.Count;

        public virtual bool IsReadOnly => false;

        public virtual void Add(T item)
        {
            lock (_locker)
            {
                if (!_items.Contains(item))
                    _items.Add(item);
            }
        }

        public virtual void Clear()
        {
            lock(_locker)
            {
                _items.Clear();
            }
        }

        public virtual bool Contains(T item)
        {
            lock (_locker)
            {
                return _items.Contains(item);
            }
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            lock (_locker)
            {
                _items.CopyTo(array, arrayIndex);
            }
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            lock (_locker)
            {
                return _items.GetEnumerator();
            }
        }

        public virtual int IndexOf(T item)
        {
            lock (_locker)
            {
                return _items.IndexOf(item);
            }
        }

        public virtual void Insert(int index, T item)
        {
            lock(_locker)
            {
                if (index < 0 || index >= Count || item == null || _items.Contains(item))
                    return;
                _items.Insert(index, item);
            }
        }

        public virtual bool Remove(T item)
        {
            lock (_locker)
            {
                if (_items.Contains(item))
                    return _items.Remove(item);
                return false;
            }
        }

        public virtual void RemoveAt(int index)
        {
            lock (_locker)
            {
                if (index >= 0 && index < Count)
                    _items.RemoveAt(index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_locker)
            {
                return _items.GetEnumerator();
            }
        }
    }
}
