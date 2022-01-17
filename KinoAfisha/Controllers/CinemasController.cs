using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using KinoAfisha.Models;
using WebAppAspNetMvcCodeFirst.Extensions;



namespace KinoAfisha.Controllers
{
    public class CinemasController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var cinemas = db.Cinemas.ToList();

            return View(cinemas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var cinema = new Cinema();
            return View(cinema);
        }

        [HttpPost]
        public ActionResult Create(Cinema model)
        {
            if (!ModelState.IsValid)
                return View(model);
   
            var db = new KinoAfishaContext();

            db.Cinemas.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Cinemas/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var cinema = db.Cinemas.FirstOrDefault(x => x.Id == id);
            if (cinema == null)
                return RedirectPermanent("/Cinemas/Index");

            db.Cinemas.Remove(cinema);
            db.SaveChanges();

            return RedirectPermanent("/Cinemas/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var cinema = db.Cinemas.FirstOrDefault(x => x.Id == id);
            if (cinema == null)
                return RedirectPermanent("/Cinemas/Index");

            return View(cinema);
        }

        [HttpPost]
        public ActionResult Edit(Cinema model)
        {
            var db = new KinoAfishaContext();
            var cinema = db.Cinemas.FirstOrDefault(x => x.Id == model.Id);
            if (cinema == null)
                ModelState.AddModelError("Id", "Кинотеатр не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingFilm(model, cinema, db);

            db.Entry(cinema).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Cinemas/Index");
        }

        private void MappingFilm(Cinema sourse, Cinema destination, KinoAfishaContext db)
        {
          
            destination.CinemaPlace = sourse.CinemaPlace;
            destination.NumberOfBilets = sourse.NumberOfBilets;
            destination.QRcode = sourse.QRcode;

        }
        

        }
    }

