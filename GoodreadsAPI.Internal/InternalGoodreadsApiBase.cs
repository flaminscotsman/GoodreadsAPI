using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GoodreadsAPI.Internal.Models;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal
{
    public class InternalGoodreadsApiBase : IInternalGoodreadsApi
    {
        private readonly string _developerKey;
        protected HttpClient Client = new HttpClient();

        public InternalGoodreadsApiBase(string developerKey)
        {
            _developerKey = developerKey;
        }

        public virtual async Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForAuthor(int authorId, int page = 1)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/xml")},
                },
                RequestUri = new Uri($"https://www.goodreads.com/author/list/{authorId}?format=xml&key={_developerKey}&page={page}")
            };
            var response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to load author_show for author #{authorId}");
            }

            var serializer = new XmlSerializer(typeof(BookListEnvelopeDto));
            var unpackedObject =
                serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as BookListEnvelopeDto;

            if (unpackedObject == null)
                throw new Exception($"Failed to decode books for author #{authorId}");

            var books = unpackedObject.Author.Wrapper.Items
                .Select(o => new GhostedBook(this, o.Id)
                {
                    AverageRating = o.AverageRating,
                    EditionInformation = o.EditionInformation,
                    EditionPublishedDate = o.EditionPublishedDate.GetValueOrDefault(),
                    Format = o.Format,
                    GoodreadsUrl = o.GoodreadsUrl,
                    Images = o.Images,
                    Pages = o.Pages,
                    RatingCount = o.RatingCount,
                    ReviewCount = o.ReviewCount,
                    Title = o.Title,
                    WorkId = o.WorkId,
                });

            return new Tuple<int, IEnumerable<IBook>>(unpackedObject.Author.Wrapper.Count, books);

        }

        public virtual Task<Tuple<int, IEnumerable<IBook>>> LoadBooksForWork(int authorId, int page = 1)
        {
            throw new Exception("Do not have access to load the books corresponding to works.");
        }

        public virtual async Task<AuthorDto> GetInternalAuthor(int authorId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/xml")},
                },
                RequestUri = new Uri($"https://www.goodreads.com/author/show/{authorId}?format=xml&key={_developerKey}")
            };
            var response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to load author_show for author #{authorId}");
            }

            var serializer = new XmlSerializer(typeof(AuthorEnvelopeDto));
            var unpackedObject =
                serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as AuthorEnvelopeDto;

            return unpackedObject?.Author;
        }

        public virtual async Task<BookDto> GetInternalBook(int bookId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/xml")},
                },
                RequestUri = new Uri($"https://www.goodreads.com/book/show/{bookId}?format=xml&key={_developerKey}")
            };
            var response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to load author_show for author #{bookId}");
            }

            var serializer = new XmlSerializer(typeof(BookEnvelopeDto));
            var unpackedObject =
                serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as BookEnvelopeDto;

            return unpackedObject?.Book;
        }

        public virtual async Task<SeriesDto> GetInternalSeries(int seriesId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/xml")},
                },
                RequestUri = new Uri($"https://www.goodreads.com/series/{seriesId}?format=xml&key={_developerKey}")
            };
            var response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to load series_show for series #{seriesId}");
            }

            var serializer = new XmlSerializer(typeof(SeriesEnvelopeDto));
            var unpackedObject =
                serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as SeriesEnvelopeDto;

            return unpackedObject?.Series;
        }

        public virtual async Task<IEnumerable<SeriesForWorkDto>> GetSeriesForWork(int workId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    Accept = { MediaTypeWithQualityHeaderValue.Parse("application/xml")},
                },
                RequestUri = new Uri($"https://www.goodreads.com/work/{workId}/series?format=xml&key={_developerKey}")
            };
            var response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to load series_show for series #{workId}");
            }

            var serializer = new XmlSerializer(typeof(SeriesForWorkEnvelopeDto));
            var xml = await response.Content.ReadAsStringAsync();
            var unpackedObject =
                serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml))) as SeriesForWorkEnvelopeDto;

            return unpackedObject?.Series.Series.Where(o => o.Id > 0).ToList();
        }
    }
}
