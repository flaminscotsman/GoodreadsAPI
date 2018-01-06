using System.Xml.Serialization;

namespace GoodreadsAPI.Internal.Models
{
    public class BookListDto : CommonBookDto
    {
        [XmlElement("title_without_series")] public string TitleWithoutSeries { get; set; }
    }
}
