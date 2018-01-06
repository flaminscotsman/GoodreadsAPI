using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI
{
    public interface IInternalGoodreadsApi
    {
        Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForAuthor(int authorId, int page);
        Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForWork(int workId, int page);
        Task<AuthorDto> GetInternalAuthor(int authorId);
        Task<BookDto> GetInternalBook(int bookId);
        Task<SeriesDto> GetInternalSeries(int seriesId);
        Task<IEnumerable<SeriesForWorkDto>> GetSeriesForWork(int workId);
    }
}
