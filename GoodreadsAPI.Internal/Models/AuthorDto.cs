using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using GoodreadsAPI.Models;

namespace GoodreadsAPI.Internal.Models
{
    public class AuthorDto
    {
        [XmlElement("id")] public int Id { get; set; }

        [XmlElement("name")] public string Name { get; set; }

        [XmlIgnore] public ImageLocations Images { get; } = new ImageLocations();

        [XmlIgnore] public Uri GoodreadsUrl { get; set; }

        [XmlElement("about")] public string About { get; set; }

        [XmlElement("influences")] public string Influences { get; set; }

        [XmlElement("gender")] public string Gender { get; set; }

        [XmlElement("hometown")] public string Hometown { get; set; }

        [XmlIgnore] public DateTime? BornDate { get; set; }

        [XmlIgnore] public DateTime? DiedDate { get; set; }


        [XmlElement("uri")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string GoodreadsUrlString
        {
            get => GoodreadsUrl?.ToString();
            set => GoodreadsUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("large_image_url")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string LargeImageUrl
        {
            get => Images.LargeImageUrl?.ToString();
            set => Images.LargeImageUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("image_url")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ImageUrl
        {
            get => Images.ImageUrl?.ToString();
            set => Images.ImageUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("small_image_url")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SmallImageUrl
        {
            get => Images.SmallImageUrl?.ToString();
            set => Images.SmallImageUrl = string.IsNullOrWhiteSpace(value.Trim()) ? null : new Uri(value);
        }

        [XmlElement("born_at")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string BornAtString
        {
            get => BornDate?.ToString(CultureInfo.InvariantCulture);
            set
            {
                if (!string.IsNullOrWhiteSpace(value.Trim()))
                    BornDate = DateTime.Parse(value.Trim());
            }
        }

        [XmlElement("died_at")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string DiedAtString
        {
            get => DiedDate?.ToString(CultureInfo.InvariantCulture);
            set
            {
                if (!string.IsNullOrWhiteSpace(value.Trim()))
                    DiedDate = DateTime.Parse(value.Trim());
            }
        }
    }
}