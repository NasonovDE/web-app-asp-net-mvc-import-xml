using System.Xml.Serialization;

namespace KinoAfisha.Models.Xml
{
    public class XmlFilm
    {
        /// <summary>
        /// Id
        /// </summary> 
        [XmlElement("Id")]
        public int Id { get; set; }
    }
}