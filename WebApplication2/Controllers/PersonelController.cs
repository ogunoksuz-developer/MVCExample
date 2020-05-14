using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Entity;
using System.Data.Entity;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    

    public class PersonelController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList();
            return View(model);
        }

        [Authorize(Roles = "A")]
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel=new Personel()
            };
           
            return View("PersonelFrom",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel=personel
                };

                return View("PersonelForm", model);
            }
            if(personel.Id==0)
            {
                db.Personel.Add(personel);
            }
            else
            {
                //tek tek yazmak yerine 
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGuncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar=db.Departman.ToList(),
                Personel=db.Personel.Find(id)
            };
            return View("PersonelFrom", model);

        }
        public ActionResult Sil(int id)
        {
            var silinecekpersonel = db.Personel.Find(id);
            if (silinecekpersonel == null)
                return HttpNotFound();

            db.Personel.Remove(silinecekpersonel);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x => x.DepartamanId == id).ToList();
            return PartialView(model);
        }
    }
}