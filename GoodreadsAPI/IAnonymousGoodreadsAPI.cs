using System.Threading.Tasks;
using GoodreadsAPI.Models;

namespace GoodreadsAPI
{
    public interface IAnonymousGoodreadsApi
    {
        Task<IAuthor> GetAuthor(int authorId, bool eagerLoadBooks = false);
        Task<IBook> GetBook(int bookId);
    }
}
