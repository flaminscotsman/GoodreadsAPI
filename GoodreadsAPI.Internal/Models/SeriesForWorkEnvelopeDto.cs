using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    public class ShimSeriesList
    {
        [XmlElement("series_work")] public List<SeriesForWorkDto> Series { get; set; }
    }

    [XmlRoot("GoodreadsResponse")]
    public class SeriesForWorkEnvelopeDto
    {
        [XmlAnyElement()] public object Result { get; set; }
        [XmlElement("series_works")] public ShimSeriesList Series { get; set; }
    }
}
