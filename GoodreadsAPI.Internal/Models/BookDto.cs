using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    public class BookDto : CommonBookDto
    {
        public class ShimAuthor
        {
            [XmlElement("id")] public int Id { get; set; }
            [XmlElement("name")] public string Name { get; set; }
        }

        [XmlElement("asin")] public string Asin { get; set; }
        [XmlElement("kindle_asin")] public string KindleAsin { get; set; }
        [XmlElement("marketplace_id")] public string MarketplaceId { get; set; }
        [XmlElement("country_code")] public string CountryCode { get; set; }
        [XmlElement("language_code")] public string LanguageCode { get; set; }
        [XmlElement("is_ebook")] public string IsEbook { get; set; }
        [XmlElement("authors")] public List<ShimAuthor> Authors { get; set; }
    }
}
