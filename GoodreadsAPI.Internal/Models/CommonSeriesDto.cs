using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    public class CommonSeriesDto
    {
        [XmlElement("id")] public int Id { get; set; }

        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("description")] public string Description { get; set; }

        [XmlElement("note")] public string Note { get; set; }

        [XmlElement("series_works_count")] public byte WorksCount { get; set; }

        [XmlElement("primary_work_count")] public byte PrimaryWorkCount { get; set; }

        [XmlElement("numbered")] public bool Numbered { get; set; }
    }
}
