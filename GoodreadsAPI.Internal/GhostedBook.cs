using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    public class GhostedBook : IBook
    {
        private readonly IInternalGoodreadsApi _api;
        private BookDto _book;

        private int? _workId;
        private IReadOnlyCollection<IAuthor> _authors;
        private BookIdentifiers _identitfiers;
        private string _title;
        private ImageLocations _images;
        private Uri _goodreadsUrl;
        private int? _pages;
        private string _format;
        private string _editionInformation;
        private DateTime? _editionPublishedDate;
        private float? _averageRating;
        private int? _ratingCount;
        private int? _reviewCount;
        private IWork _work;

        private BookDto FullBook
        {
            get
            {
                if (_book == null)
                {
                    var task = _api.GetInternalBook(Id);
                    _book = task.Result;
                }

                return _book;
            }
        }

        public GhostedBook(IInternalGoodreadsApi api, int bookId)
        {
            _api = api;
            Id = bookId;
        }
        public GhostedBook(IInternalGoodreadsApi api, int bookId, BookDto internalBook)
        {
            _api = api;
            Id = bookId;
            _book = internalBook;
        }

        public int Id { get; }

        public int WorkId
        {
            get => _workId ?? FullBook.WorkId;
            set => _workId = value;
        }

        public IWork Work
        {
            get => _work ?? (_work = new GhostedWork(_api, WorkId)
            {
                BestBookId = FullBook.Work.BestBookId,
                NumberOfBooks = FullBook.Work.NumberOfBooks
            });
            set => _work = value;
        }

        public IReadOnlyCollection<IAuthor> Authors
        {
            get
            {
                if (_authors == null)
                {
                    _authors = FullBook.Authors.Select(e => new GhostedAuthor(_api, e.Id)
                    {
                        Name = e.Name
                    }).ToImmutableList();
                }

                return _authors;
            }
            set => _authors = value;
        }

        public BookIdentifiers Identitfiers
        {
            get
            {
                if (_identitfiers == null)
                {
                    _identitfiers = new BookIdentifiers()
                    {
                        Asin = FullBook.Asin,
                        Isbn = FullBook.Asin,
                        Isbn13 = FullBook.Isbn13,
                        KindleAsin = FullBook.KindleAsin,
                        MarketplaceId = FullBook.MarketplaceId
                    };
                }

                return _identitfiers;
            }
            set => _identitfiers = value;
        }

        public string Title
        {
            get => _title ?? FullBook.Title;
            set => _title = value;
        }

        public ImageLocations Images
        {
            get => _images ?? FullBook.Images;
            set => _images = value;
        }

        public Uri GoodreadsUrl
        {
            get => _goodreadsUrl ?? FullBook.GoodreadsUrl;
            set => _goodreadsUrl = value;
        }

        public int Pages
        {
            get => _pages ?? FullBook.Pages;
            set => _pages = value;
        }

        public string Format
        {
            get => _format ?? FullBook.Format;
            set => _format = value;
        }

        public string EditionInformation
        {
            get => _editionInformation ?? FullBook.EditionInformation;
            set => _editionInformation = value;
        }

        public DateTime EditionPublishedDate
        {
            get => _editionPublishedDate ?? FullBook.EditionPublishedDate.GetValueOrDefault();
            set => _editionPublishedDate = value;
        }

        public float AverageRating
        {
            get => _averageRating ?? FullBook.AverageRating;
            set => _averageRating = value;
        }

        public int RatingCount
        {
            get => _ratingCount ?? FullBook.RatingCount;
            set => _ratingCount = value;
        }

        public int ReviewCount
        {
            get => _reviewCount ?? FullBook.ReviewCount;
            set => _reviewCount = value;
        }
    }
}
