using System;
using System.Collections.Generic;

namespace GoodreadsAPI.Models
{
    public interface IAuthor
    {
        int Id { get; }
        string Name { get; }
        ImageLocations Images { get; }
        Uri GoodreadsUrl { get; }
        string About { get; }
        string Influences { get; }
        string Gender { get; }
        string Hometown { get; }
        DateTime? BornDate { get; }
        DateTime? DiedDate { get; }
        IReadOnlyCollection<IBook> Books { get; }
    }
}
