using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    [XmlRoot("GoodreadsResponse")]
    public class BookListEnvelopeDto
    {
        public class BookListWrapper
        {
            [XmlAttribute("total")]
            public int Count { get; set; }

            [XmlElement("book")]
            public List<BookListDto> Items { get; set; }
        }

        public class AuthorStub
        {
            [XmlElement("books")]
            public BookListWrapper Wrapper { get; set; }
        }

        [XmlAnyElement()]
        public object Request { get; set; }
        [XmlElement("author")]
        public AuthorStub Author { get; set; }
    }
}
