using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    public abstract class LazyBooksCollectionBase : IReadOnlyCollection<IBook>
    {
        private IBook[] _booksCache;
        private int _loadedBooks;
        private int _count;

        protected LazyBooksCollectionBase(IInternalGoodreadsApi api)
        {
            Api = api;
        }

        protected LazyBooksCollectionBase(IInternalGoodreadsApi api, int capacity, IEnumerable<IBook> initial)
        {
            Api = api;
            Count = capacity;

            AddBooks(initial);
        }
        protected IInternalGoodreadsApi Api { get; }

        protected bool Initialised { get; private set; }

        public int Count
        {
            get => _count;
            protected set
            {
                if (Initialised)
                {
                    throw new Exception("Cannot reinitialise a LazyBookCollection");
                }
                _count = value;
                _booksCache = new IBook[value];
                Initialised = true;
            }
        }

        public bool IsReadOnly { get; } = true;

        public IEnumerator<IBook> GetEnumerator()
        {
            if (!Initialised)
            {
                if (!LoadNextBatch().Result) throw new Exception($"Failed to lazily evaluate avaialable books!");
            }
            return new BookCollectionEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract Task<bool> LoadNextBatch();

        protected void AddBooks(IEnumerable<IBook> books)
        {
            foreach (var book in books) _booksCache[_loadedBooks++] = book;
        }

        private class BookCollectionEnumerator : IEnumerator<IBook>
        {
            private readonly LazyBooksCollectionBase _collection;
            private int _index = -1;

            public BookCollectionEnumerator(LazyBooksCollectionBase collection)
            {
                _collection = collection;
            }

            public bool MoveNext()
            {
                if (_index + 1 <= _collection.Count) _index++;

                return _index + 1 <= _collection.Count;
            }

            public void Reset()
            {
                _index = 0;
            }

            public IBook Current
            {
                get
                {
                    if (_index >= _collection._loadedBooks)
                    {
                        var task = _collection.LoadNextBatch();
                        if (!task.Result) throw new Exception($"Failed to lazily load book #{_index}");
                    }

                    return _collection._booksCache[_index];
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}
