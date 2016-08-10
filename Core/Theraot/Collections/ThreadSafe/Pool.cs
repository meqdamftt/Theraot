﻿// Needed for NET40

using System;

namespace Theraot.Collections.ThreadSafe
{
    internal class Pool<T>
        where T : class
    {
        private readonly int _id;
        private readonly FixedSizeQueue<T> _entries;
        private readonly Action<T> _recycler;

        public Pool(int capacity)
        {
            _id = ThreadLocalFlagHelper.GetId();
            _entries = new FixedSizeQueue<T>(capacity);
            _recycler = GC.KeepAlive;
        }

        public Pool(int capacity, Action<T> recycler)
        {
            if (recycler == null)
            {
                throw new ArgumentNullException("recycler");
            }
            _id = ThreadLocalFlagHelper.GetId();
            _entries = new FixedSizeQueue<T>(capacity);
            _recycler = recycler;
        }

        internal bool Donate(T entry)
        {
            if (!ReferenceEquals(entry, null) && ThreadLocalFlagHelper.Enter(_id))
            {
                try
                {
                    _recycler.Invoke(entry);
                    _entries.Add(entry);
                    return true;
                }
                catch (NullReferenceException exception)
                {
                    GC.KeepAlive(exception);
                }
                finally
                {
                    ThreadLocalFlagHelper.Leave(_id);
                }
            }
            return false;
        }

        internal bool TryGet(out T result)
        {
            return _entries.TryTake(out result);
        }
    }
}