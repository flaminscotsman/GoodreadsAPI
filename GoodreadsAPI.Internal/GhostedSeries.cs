using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    class GhostedSeries : ISeries
    {
        private readonly IInternalGoodreadsApi _api;
        private SeriesDto _series;

        private bool? _isOrdered;
        private string _title;
        private string _description;
        private string _note;
        private int? _worksCount;
        private int? _primaryWorksCount;
        private IReadOnlyCollection<IWork> _works;


        public GhostedSeries(IInternalGoodreadsApi api, int seriesId)
        {
            _api = api;
            Id = seriesId;
        }

        private SeriesDto FullSeries => _series ?? (_series = _api.GetInternalSeries(Id).Result);

        public int Id { get; }

        public bool IsOrdered
        {
            get => _isOrdered ?? FullSeries.Numbered;
            set => _isOrdered = value;
        }

        public string Title
        {
            get => _title ?? FullSeries.Title;
            set => _title = value;
        }

        public string Description
        {
            get => _description ?? FullSeries.Description;
            set => _description = value;
        }

        public string Note
        {
            get => _note ?? FullSeries.Note;
            set => _note = value;
        }

        public int WorksCount
        {
            get => _worksCount ?? FullSeries.WorksCount;
            set => _worksCount = value;
        }

        public int PrimaryWorksCount
        {
            get => _primaryWorksCount ?? FullSeries.PrimaryWorkCount;
            set => _primaryWorksCount = value;
        }

        public IReadOnlyCollection<IWork> Works
        {
            get
            {
                if (_works == null)
                {
                    _works = FullSeries.SeriesWorks.SeriesWorks
                        .OrderBy(o  => o.Position ?? int.MaxValue)
                        .Select(element =>
                        {
                            var work = element.Work;
                            return new GhostedWork(_api, work.Id)
                            {
                                BestBookId = work.BestBook.Id,
                                BestBook = new GhostedBook(_api, work.BestBook.Id)
                                {
                                    Title = work.BestBook.Title
                                },
                                NumberOfBooks = work.Books
                            };
                        })
                        .ToImmutableList();
                }
                return _works;
            }
            set => _works = value;
        }
    }
}
