using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entity;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();

        [Authorize]
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm",new Departman());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman dep)
        {
            if (!ModelState.IsValid)
                return View("DepartmanForm");

            MesajViewModel model = new MesajViewModel();
            if (dep.Id == 0)
            {
                db.Departman.Add(dep);
                model.Mesaj = dep.Ad + " başarıyla eklendi";
            }
                
            else
            {
                var guncellenecekdepartman = db.Departman.Find(dep.Id);
                    if (guncellenecekdepartman == null) 
                    return HttpNotFound(); 

                guncellenecekdepartman.Ad = dep.Ad;
                model.Mesaj = dep.Ad + " başarıyla güncellendi";

            }
            db.SaveChanges();
            model.Status = true;
            model.Url = "/Departman";
            model.LinkText = "Departman Listesi";
            return View("_Mesaj",model);
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekdepartman = db.Departman.Find(id);
            if (silinecekdepartman == null)
                return HttpNotFound();
            
            db.Departman.Remove(silinecekdepartman);
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");
        }
    }
}