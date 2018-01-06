using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    [XmlRoot("GoodreadsResponse")]
    public class BookEnvelopeDto
    {
        [XmlAnyElement()]
        public object Request { get; set; }
        [XmlElement("book")]
        public BookDto Book { get; set; }
    }
}
