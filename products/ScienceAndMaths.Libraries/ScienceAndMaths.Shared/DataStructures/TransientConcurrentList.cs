using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ScienceAndMaths.Shared.DataStructures
{
    /// <summary> Represents a thread-safe collection of items that get removed after an expiry time and can be operated by multiple threads concurrently.
    /// <para>Thread-safe operations include TryAdd, Contains, Remove.</para>
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    public class TransientConcurrentList<T> : IList<T>
    {
        private readonly ConcurrentDictionary<T, DateTime> internalDictionary = new ConcurrentDictionary<T, DateTime>();
        private readonly Timer timer;
        private readonly TimeSpan expiryTime;

        public int Count => internalDictionary.Count;
        public bool IsReadOnly => false;

        public TransientConcurrentList(TimeSpan expiryTime)
        {
            this.expiryTime = expiryTime;
            timer = new Timer(RemoveExpiredItems, null, this.expiryTime, this.expiryTime);
        }

        public bool TryAdd(T item)
        {
            return internalDictionary.TryAdd(item, DateTime.Now);
        }

        private void RemoveExpiredItems(object state)
        {
            var now = DateTime.Now;
            foreach (var item in internalDictionary)
            {
                if ((now - item.Value) >= expiryTime)
                {
                    internalDictionary.TryRemove(item.Key, out _);
                }
            }
        }

        public void Stop()
        {
            timer.Dispose();
        }

        public void Add(T item)
        {
            if(!TryAdd(item))
            {
                throw new InvalidOperationException("Could not safely add item to list!");
            }
        }

        public void Clear()
        {
            internalDictionary.Clear();
        }

        public bool Contains(T item)
        {
            return internalDictionary.TryGetValue(item , out _);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            internalDictionary.Keys.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return internalDictionary.TryRemove(item, out _);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalDictionary.Keys.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return internalDictionary.Keys.ToList().IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (!TryAdd(item))
            {
                throw new InvalidOperationException("Could not safely add item to list!");
            }
        }

        public void RemoveAt(int index)
        {
            T item = internalDictionary.Keys.ToList()[index];

            internalDictionary.TryRemove(item, out _);
        }

        public T this[int index]
        {
            get => internalDictionary.Keys.ToList()[index];
            set => internalDictionary.Keys.ToList()[index] = value;
        }
    }
}
