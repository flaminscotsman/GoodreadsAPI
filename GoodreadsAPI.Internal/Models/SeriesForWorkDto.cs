using System.Collections.Generic;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    public class SeriesForWorkDto
    {
        [XmlElement("id")] public int Id { get; set; }
        [XmlElement("user_position")] public int Order { get; set; }
        [XmlElement("series")] public CommonSeriesDto Series { get; set; }
    }
}
