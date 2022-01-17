using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using KinoAfisha.Models;
using KinoAfisha.Models.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

using System.Xml;




namespace KinoAfisha.Controllers
{
    public class KinosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.ToList();


            return View(kino);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var kino = new Kino();
            return View(kino);

        }

        [HttpPost]
        public ActionResult Create(Kino model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new KinoAfishaContext();

           //model.KinoTime = DateTime.Now;
           
            //model.NextArrivalDate = DateTime.Now;
           

            if (model.FilmIds != null && model.FilmIds.Any())
            {
                var film = db.Films.Where(s => model.FilmIds.Contains(s.Id)).ToList();
                model.Films = film;
            }
            if (model.CinemaIds != null && model.CinemaIds.Any())
            {
                var cinema = db.Cinemas.Where(s => model.CinemaIds.Contains(s.Id)).ToList();
                model.Cinemas = cinema;
            }



            db.Kinos.Add(model);
            db.SaveChanges();


            return RedirectPermanent("/Kinos/Index");
        }
        
         
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == id);
            if (kino == null)
                return RedirectPermanent("/Kinos/Index");

            db.Kinos.Remove(kino);
            db.SaveChanges();

            return RedirectPermanent("/Kinos/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == id);

            if (kino == null)
                return RedirectPermanent("/Kinos/Index");

            return View(kino);
        }

        [HttpPost]
        public ActionResult Edit(Kino model)
        {

            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == model.Id);

            

            if (kino == null)
            {
                ModelState.AddModelError("Id", "кино не найдено");
            }
            if (!ModelState.IsValid)
                return View(model);
            
            MappingKino(model, kino, db);
            
            db.Entry(kino).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectPermanent("/Kinos/Index");
        }


        private void MappingKino(Kino sourse, Kino destination, KinoAfishaContext db)
        {

            destination.Price = sourse.Price;
            //destination.NextArrivalDate = sourse.NextArrivalDate;
            destination.StringKinoTime = sourse.StringKinoTime;
            destination.KinoDate = sourse.KinoDate;
            destination.FilmIds = sourse.FilmIds;




            if (destination.Films != null)
                destination.Films.Clear();

            if (sourse.FilmIds != null && sourse.FilmIds.Any())
                destination.Films = db.Films.Where(s => sourse.FilmIds.Contains(s.Id)).ToList();

            if (destination.Cinemas != null)
                destination.Cinemas.Clear();

            if (sourse.CinemaIds != null && sourse.CinemaIds.Any())
                destination.Cinemas = db.Cinemas.Where(s => sourse.CinemaIds.Contains(s.Id)).ToList();


        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var db = new KinoAfishaContext();
            var kino = db.Kinos.FirstOrDefault(x => x.Id == id);
            if (kino == null)
                return RedirectPermanent("/Kinos/Index");

            return View(kino);
        }


        [HttpGet]
        public ActionResult GetXml()
        {
            var db = new KinoAfishaContext();
            var kinos = db.Kinos.ToList().Select(x => new XmlKino()
            {
                Price = (int)x.Price,
                KinoDate = x.KinoDate,
                KinoTime = x.StringKinoTime,
                Cinemas = x.Cinemas.Select(y => new XmlCinema() { Id = y.Id }).ToList(),
                Films = x.Films.Select(y => new XmlFilm() { Id = y.Id }).ToList(),

               
            }).ToList();



            XmlSerializer xml = new XmlSerializer(typeof(List<XmlKino>));
            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ms = new MemoryStream();
            xml.Serialize(ms, kinos, ns);
            ms.Position = 0;

            return File(new MemoryStream(ms.ToArray()), "text/xml");
        }
    }
}
