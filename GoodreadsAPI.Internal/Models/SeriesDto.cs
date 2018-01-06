using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    public class SeriesDto : CommonSeriesDto
    {
        [XmlElement("series_works")] public SeriesWorkShim SeriesWorks { get; set; }
    }
    
    public class SeriesWorkShim
    {
        [XmlElement("series_work")] public List<SeriesWorkDto> SeriesWorks { get; set; }
    }


    public class SeriesWorkDto
    {
        [XmlElement("id")] public int Id { get; set; }

        [XmlIgnore]
        public int? Position
        {
            get
            {
                if (int.TryParse(RawPosition, out var val))
                    return val;
                return null;
            }
        }

        [XmlElement("work")] public ShimWorkDto Work { get; set; }
        [XmlElement("user_position")] public string RawPosition { get; set; }
    }
    

    public class ShimWorkDto
    {
        [XmlElement("id")] public int Id { get; set; }

        [XmlElement("best_book")] public ShimBestBookDto BestBook { get; set; }

        [XmlElement("books_count")] public int Books { get; set; }

        [XmlElement("original_title")] public string OriginalTitle { get; set; }

//        [XmlElement("ratings_count")] public int? RatingsCount { get; set; }
//        
//        [XmlElement("ratings_sum")] public int? RatingsSum { get; set; }
//        
//        [XmlElement("reviews_count")] public int ReviewsCount { get; set; }
//         
//        [XmlElement("text_reviews_count")] public int TextReviewsCount { get; set; }
//
//        [XmlElement("average_rating")] public float AverageRating { get; set; }

        [XmlIgnore]
        public DateTime? OriginalPublicationDate
        {
            get
            {
                if (int.TryParse(OriginalPublicationDay, out var day) && int.TryParse(OriginalPublicationMonth, out var month) && int.TryParse(OriginalPublicationYear, out var year))
                    return new DateTime(year, month, day);
                return null;
            }
        }

        [XmlElement("original_publication_day")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string OriginalPublicationDay { get; set; }

        [XmlElement("original_publication_month")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string OriginalPublicationMonth { get; set; }

        [XmlElement("original_publication_year")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string OriginalPublicationYear { get; set; }
    }


    public class ShimBestBookDto
    {
        [XmlElement("id")] public int Id { get; set; }

        [XmlElement("title")] public string Title { get; set; }
    }
    
}
