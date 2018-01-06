using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodreadsAPI.Internal;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Impl
{
    public class GoodreadsApi : IGoodreadsApi, IInternalGoodreadsApi
    {
        private IInternalGoodreadsApi _internalApi;

        public GoodreadsApi(IInternalGoodreadsApi internalApi)
        {
            _internalApi = internalApi;
        }

        public async Task<IAuthor> GetAuthor(int authorId, bool eagerLoadBooks = false)
        {
            var internalAuthor = await GetInternalAuthor(authorId);

            if (eagerLoadBooks)
            {
                var booksForAuthor = await LoadBooksForAuthor(authorId);

                return new GhostedAuthor(this, authorId, internalAuthor,
                    new LazyAuthorBooksCollection(this, authorId, booksForAuthor.Item1, booksForAuthor.Item2));
            }
            else
            {
                return new GhostedAuthor(this, authorId, internalAuthor);
            }
        }

        public async Task<IBook> GetBook(int bookId)
        {
            return new GhostedBook(this, bookId, await GetInternalBook(bookId));
        }
        public Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForAuthor(int authorId, int page=1)
        {
            return _internalApi.LoadBooksForAuthor(authorId, page);
        }
        public Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForWork(int workId, int page=1)
        {
            return _internalApi.LoadBooksForWork(workId, page);
        }

        public Task<AuthorDto> GetInternalAuthor(int authorId)
        {
            return _internalApi.GetInternalAuthor(authorId);
        }

        public Task<BookDto> GetInternalBook(int bookId)
        {
            return _internalApi.GetInternalBook(bookId);
        }

        public Task<SeriesDto> GetInternalSeries(int seriesId)
        {
            return _internalApi.GetInternalSeries(seriesId);
        }

        public Task<IEnumerable<SeriesForWorkDto>> GetSeriesForWork(int workId)
        {
            return _internalApi.GetSeriesForWork(workId);
        }
    }
}
