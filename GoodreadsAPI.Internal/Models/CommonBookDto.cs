using System;
using System.ComponentModel;
using System.Xml.Serialization;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal.Models
{
    public class CommonBookDto
    {
        public class WorkWrapper
        {
            [XmlElement("id")] public int Id { get; set; }
            [XmlElement("books_count")] public int NumberOfBooks { get; set; }
            [XmlElement("best_book_id ")] public int BestBookId { get; set; }
        }

        [XmlElement("id")] public int Id { get; set; }

        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("description")] public string Description { get; set; }

        [XmlIgnore] public ImageLocations Images { get; } = new ImageLocations();

        [XmlIgnore] public Uri GoodreadsUrl { get; set; }

        [XmlIgnore] public int Pages { get; set; }

        [XmlElement("format")] public string Format { get; set; }

        [XmlElement("edition_information")] public string EditionInformation { get; set; }
        [XmlElement("publisher")] public string Publisher { get; set; }

        [XmlIgnore]
        public DateTime? EditionPublishedDate
        {
            get
            {
                if (int.TryParse(PublicationDay, out var day) && int.TryParse(PublicationMonth, out var month) && int.TryParse(PublicationYear, out var year))
                    return new DateTime(year, month, day);
                return null;
            }
        }

        [XmlElement("average_rating")] public float AverageRating { get; set; }

        [XmlElement("ratings_count")] public int RatingCount { get; set; }

        [XmlElement("text_reviews_count")] public int ReviewCount { get; set; }

        [XmlElement("isbn")] public string Isbn { get; set; }
        [XmlElement("isbn13")] public string Isbn13 { get; set; }

        [XmlIgnore] public int WorkId { get => Work.Id; }

        [XmlElement("work")] public WorkWrapper Work { get; set; }

        [XmlElement("link")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string GoodreadsUrlString
        {
            get => GoodreadsUrl?.ToString();
            set => GoodreadsUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("large_image_url")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string LargeImageUrl
        {
            get => Images.LargeImageUrl?.ToString();
            set => Images.LargeImageUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("image_url")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ImageUrl
        {
            get => Images.ImageUrl?.ToString();
            set => Images.ImageUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("small_image_url")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SmallImageUrl
        {
            get => Images.SmallImageUrl?.ToString();
            set => Images.SmallImageUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("publication_day")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PublicationDay { get; set; }

        [XmlElement("publication_month")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PublicationMonth { get; set; }

        [XmlElement("publication_year")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PublicationYear { get; set; }

        [XmlElement("num_pages")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string RawPages
        {
            get => Pages.ToString();
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    Pages = int.Parse(value);
            }
        }
    }
}