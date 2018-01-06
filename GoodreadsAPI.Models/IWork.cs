using System.Collections.Generic;

namespace GoodreadsAPI.Models
{
    public interface IWork
    {
        int Id { get; }
        int NumberOfBooks { get; }
        IBook BestBook { get; }
        IReadOnlyCollection<IBook> Books { get; }
        IReadOnlyCollection<ISeries> Series { get; }
    }
}
