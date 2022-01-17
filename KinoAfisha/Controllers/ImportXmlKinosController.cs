using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using KinoAfisha.Models;
using KinoAfisha.Models.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;

using System.Xml;


namespace KinoAfisha.Controllers
{
    public class ImportXmlKinosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ImportXmlKinoViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Import(ImportXmlKinoViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var file = new byte[model.FileToImport.InputStream.Length];
            model.FileToImport.InputStream.Read(file, 0, (int)model.FileToImport.InputStream.Length);

            XmlSerializer xml = new XmlSerializer(typeof(List<XmlKino>));
            var kinos = (List<XmlKino>)xml.Deserialize(new MemoryStream(file));
            var db = new KinoAfishaContext();



            foreach (var kino in kinos)
            {
                var input = new Kino()
                {                  
                    Price = kino.Price,
                    KinoDate = kino.KinoDate,
                    StringKinoTime = kino.KinoTime,

                };
                var cinemaIds = kino.Cinemas.Select(x => x.Id).ToList();
                var cinemas = db.Cinemas.Where(s => cinemaIds.Contains(s.Id)).ToList();
                input.Cinemas = cinemas;

                var filmIds = kino.Films.Select(x => x.Id).ToList();
                var films = db.Films.Where(s => filmIds.Contains(s.Id)).ToList();
                input.Films = films;

              



                db.Kinos.Add(input);

                db.SaveChanges();
            }

            return RedirectPermanent("/Kinos/Index");
        }

        public ActionResult GetExample()
        {
            return File("~/Content/Files/ImportXmlKinosExample.xml", "application/xml", "ImportXmlKinosExample.xml");
        }

    }
}