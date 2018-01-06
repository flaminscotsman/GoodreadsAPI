using System;
using System.Collections.Generic;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    public class GhostedAuthor : IAuthor
    {
        private readonly IInternalGoodreadsApi _api;
        private AuthorDto _author;

        private string _name;
        private ImageLocations _images;
        private Uri _goodreadsUrl;
        private string _about;
        private string _influences;
        private string _gender;
        private string _hometown;
        private DateTime? _bornDate;
        private DateTime? _diedDate;

        private AuthorDto FullAuthor
        {
            get
            {
                if (_author == null)
                {
                    _author = _api.GetInternalAuthor(Id).Result;
                }

                return _author;
            }
        }

        public GhostedAuthor(IInternalGoodreadsApi api, int authorId)
        {
            _api = api;
            Id = authorId;
            Books = new LazyAuthorBooksCollection(api, authorId);
        }

        public GhostedAuthor(IInternalGoodreadsApi api, int authorId, IReadOnlyCollection<IBook> books)
        {
            _api = api;
            Id = authorId;
            Books = books;
        }

        public GhostedAuthor(IInternalGoodreadsApi api, int authorId, AuthorDto author)
        {
            _api = api;
            _author = author;
            Id = authorId;
            Books = new LazyAuthorBooksCollection(api, authorId);
        }

        public GhostedAuthor(IInternalGoodreadsApi api, int authorId, AuthorDto author, IReadOnlyCollection<IBook> books)
        {
            _api = api;
            _author = author;
            Id = authorId;
            Books = books;
        }

        public int Id { get; }

        public string Name
        {
            get => _name ?? FullAuthor.Name;
            set => _name = value;
        }

        public ImageLocations Images
        {
            get => _images ?? FullAuthor.Images;
            set => _images = value;
        }

        public Uri GoodreadsUrl
        {
            get => _goodreadsUrl ?? FullAuthor.GoodreadsUrl;
            set => _goodreadsUrl = value;
        }

        public string About
        {
            get => _about ?? FullAuthor.About;
            set => _about = value;
        }

        public string Influences
        {
            get => _influences ?? FullAuthor.Influences;
            set => _influences = value;
        }

        public string Gender
        {
            get => _gender ?? FullAuthor.Gender;
            set => _gender = value;
        }

        public string Hometown
        {
            get => _hometown ?? FullAuthor.Hometown;
            set => _hometown = value;
        }

        public DateTime? BornDate
        {
            get => _bornDate ?? FullAuthor.BornDate;
            set => _bornDate = value;
        }

        public DateTime? DiedDate
        {
            get => _diedDate ?? FullAuthor.DiedDate;
            set => _diedDate = value;
        }

        public IReadOnlyCollection<IBook> Books { get; }
    }
}
