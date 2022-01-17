using System.Xml.Serialization;

namespace KinoAfisha.Models.Xml
{
    public class XmlCinema
    {
        /// <summary>
        /// Id
        /// </summary> 
        [XmlElement("Id")]
        public int Id { get; set; }
    }
}