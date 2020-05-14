using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models.Entity;

namespace WebApplication2.Controllers
{
    public class SecurityController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Kullanicilar kullanici)
        {
            var kullaniciInDb = db.Kullanicilar.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                return RedirectToAction("Index","Departman");
            } 
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı veya şifre hatalı !!!!!";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();  
            return View("Login");
            
        }
    }
}