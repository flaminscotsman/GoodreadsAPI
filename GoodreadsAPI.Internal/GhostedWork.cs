using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    class GhostedWork : IWork
    {
        private readonly IInternalGoodreadsApi _api;
        private IBook _bestBook;
        private IReadOnlyCollection<IBook> _books;
        private IReadOnlyCollection<ISeries> _series;

        public GhostedWork(IInternalGoodreadsApi api, int workId)
        {
            _api = api;
            Id = workId;
        }

        public int Id { get; }

        public int NumberOfBooks { get; set; }

        public int? BestBookId { get; set; }

        public IReadOnlyCollection<int> SeriesId { get; set; }

        public IBook BestBook
        {
            get
            {
                if (!BestBookId.HasValue)
                {
                    return null;
                }

                if (_bestBook == null)
                {
                    _bestBook = new GhostedBook(_api, BestBookId.Value);
                }

                return _bestBook;
            }
            set => _bestBook= value;
        }

        public IReadOnlyCollection<ISeries> Series
        {
            get
            {
                if (_series == null)
                {
                    if (SeriesId == null)
                    {
                        _series = _api.GetSeriesForWork(Id).Result
                            .Select(element =>
                            {
                                var series = element.Series;
                                return new GhostedSeries(_api, series.Id)
                                {
                                    Title = series.Title,
                                    Description = series.Description,
                                    IsOrdered = series.Numbered,
                                    Note = series.Note,
                                    PrimaryWorksCount = series.PrimaryWorkCount,
                                    WorksCount = series.WorksCount
                                };
                            }).ToImmutableList();
                    }
                    else
                    {
                        _series = (SeriesId ?? ImmutableList<int>.Empty)
                            .Select(seriesId => new GhostedSeries(_api, seriesId))
                            .ToImmutableList();
                    }
                }

                return _series;
            }
            set => _series = value;
        }

        public IReadOnlyCollection<IBook> Books
        {
            get
            {
                if (_books == null)
                {
                    _books = new LazyWorkBooksCollection(_api, Id);
                }

                return _books;
            }
        }
    }
}
