using System.Collections.Generic;
using System.Threading.Tasks;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    public class LazyAuthorBooksCollection : LazyBooksCollectionBase
    {
        private readonly int _authorId;
        private int _currentPage;

        public LazyAuthorBooksCollection(IInternalGoodreadsApi api, int authorId) : base(api)
        {
            _authorId = authorId;
            _currentPage = 0;
        }

        public LazyAuthorBooksCollection(IInternalGoodreadsApi api, int authorId, int capacity, IEnumerable<IBook> initial): base(api, capacity, initial)
        {
            _authorId = authorId;
            _currentPage = 1;
        }

        protected override async Task<bool> LoadNextBatch()
        {
            var books = await Api.LoadBooksForAuthor(_authorId, ++_currentPage);
            if (!Initialised)
            {
                Count = books.Item1;
            }

            AddBooks(books.Item2);

            return true;
        }
    }
}