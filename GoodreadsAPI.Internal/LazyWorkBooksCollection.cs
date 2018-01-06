using System.Collections.Generic;
using System.Threading.Tasks;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    public class LazyWorkBooksCollection : LazyBooksCollectionBase
    {
        private readonly int _workId;
        private int _currentPage;

        public LazyWorkBooksCollection(IInternalGoodreadsApi api, int workId) : base(api)
        {
            _workId = workId;
            _currentPage = 0;
        }

        public LazyWorkBooksCollection(IInternalGoodreadsApi api, int workId, int capacity, IEnumerable<IBook> initial): base(api, capacity, initial)
        {
            _workId = workId;
            _currentPage = 1;
        }

        protected override async Task<bool> LoadNextBatch()
        {
            var books = await Api.LoadBooksForAuthor(_workId, ++_currentPage);
            if (!Initialised)
            {
                Count = books.Item1;
            }

            AddBooks(books.Item2);

            return true;
        }
    }
}