using System;
using System.Collections.Generic;

namespace GoodreadsAPI.Models
{
    public interface IBook
    {
        int Id { get; }
        IWork Work { get; }
        IReadOnlyCollection<IAuthor> Authors { get; }
        BookIdentifiers Identitfiers { get; }
        string Title { get; }
        ImageLocations Images { get; }
        Uri GoodreadsUrl { get; }
        int Pages { get; }
        string Format { get; }
        string EditionInformation { get; }
        DateTime EditionPublishedDate { get; }
        float AverageRating { get; }
        int RatingCount { get; }
        int ReviewCount { get; }
    }
}
