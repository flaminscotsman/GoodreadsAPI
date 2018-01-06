using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    [XmlRoot("GoodreadsResponse")]
    public class AuthorEnvelopeDto
    {
        [XmlAnyElement()]
        public object Request { get; set; }
        [XmlElement("author")]
        public AuthorDto Author { get; set; }
    }
}
