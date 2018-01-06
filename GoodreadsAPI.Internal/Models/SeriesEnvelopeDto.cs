using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    [XmlRoot("GoodreadsResponse")]
    public class SeriesEnvelopeDto
    {
        [XmlAnyElement()] public object Request { get; set; }
        [XmlElement("series")] public SeriesDto Series { get; set; }
    }
}
