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
    public class FormatsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new KinoAfishaContext();
            var formats = db.Formats.ToList();

            return View(formats);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var format = new Format();
            return View(format);
        }

        [HttpPost]
        public ActionResult Create(Format model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new KinoAfishaContext();

            db.Formats.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/Formats/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new KinoAfishaContext();
            var format = db.Formats.FirstOrDefault(x => x.Id == id);
            if (format == null)
                return RedirectPermanent("/Formats/Index");

            db.Formats.Remove(format);
            db.SaveChanges();

            return RedirectPermanent("/Formats/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new KinoAfishaContext();
            var format = db.Formats.FirstOrDefault(x => x.Id == id);
            if (format == null)
                return RedirectPermanent("/Formats/Index");

            return View(format);
        }

        [HttpPost]
        public ActionResult Edit(Format model)
        {
            var db = new KinoAfishaContext();
            var format = db.Formats.FirstOrDefault(x => x.Id == model.Id);
            if (format == null)
                ModelState.AddModelError("Id", "Формат не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingNationality(model, format);

            db.Entry(format).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/Formats/Index");
        }

        private void MappingNationality(Format sourse, Format destination)
        {
            destination.Name = sourse.Name;
        }
    }
}