using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAspNetMvcCodeFirst.Extensions;
using System.Xml.Linq;
using WebAppAspNetMvcCodeFirst.Models.Attributes;
using System.Xml.Serialization;


namespace KinoAfisha.Models.Xml
{
    [XmlRoot("Kino")]
    public class XmlKino
    {
        /// <summary>
        /// Название фильма
        /// </summary>    
        [XmlArray("Films")]
        [XmlArrayItem(typeof(XmlFilm), ElementName = "Film")]
        public virtual List<XmlFilm> Films { get; set; }


        /// <summary>
        /// Место показа
        /// </summary> 
        [XmlArray("Cinemas")]
        [XmlArrayItem(typeof(XmlCinema), ElementName = "Cinema")]
        public virtual List<XmlCinema> Cinemas { get; set; }
    

        /// <summary>
        /// Цена
        /// </summary> 
        [XmlElement("Price")]
        public int Price { get; set; }

        /// <summary>
        /// Дата сеанса
        /// </summary> 
        [XmlElement("KinoDate")]
        public string KinoDate { get; set; }

        /// <summary>
        /// Время сеанса
        /// </summary> 
        [XmlElement("KinoTime")]
        public string KinoTime { get; set; }

        ///// <summary>
        ///// Дата сеанса
        ///// </summary> 
        //[XmlElement("NextArrivalDate")]
        //public string NextArrivalDate { get; set; }

        // /// <summary>
        // /// Время сеанса
        // /// </summary> 
        //[XmlElement(DataType= "KinoTime")]    
        // public DateTime? KinoTime { get; set; }




    }
}