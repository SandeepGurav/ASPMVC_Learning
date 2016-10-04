using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPMVC_ShoppingCart.Models;
using ASPMVC_ShoppingCart.Security;

namespace ASPMVC_ShoppingCart.Controllers
{
   
    public class UsersController : Controller
    {
        private ASPMVCEntities db = new ASPMVCEntities();

        // GET: Users
         [AllowAnonymous]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City);
            
            return View(users.ToList());

            
        }

        // GET: Users/Details/5
        [CustomAuthorizeFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public User Find(string emailid)
        {
            //if (emailid == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = db.Users.Find(emailid);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            return db.Users.Single(u=>u.EmailID.Equals(emailid));
        }
        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            return View();
        }

        public ActionResult BindCity(int id )
        {        
            var cities = new SelectList(db.Cities.Where(c=>c.CountryID==id), "CityID", "CityName");
            //var cityList=from c in db.Cities
            //             where c.CityID==id
            //             select new {c.CityName,c.CityID};
            
            //var cities = new SelectList(cityList, "CityID", "CityName");
            return Json(cities.ToList(), JsonRequestBehavior.AllowGet);
          
           // return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name,EmailID,ContactNo,CountryID,CityID,Password")] User user)
        {

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", user.CityID);
            return View(user);
        }
        // GET: Users/Login
       [AllowAnonymous]
        public ActionResult Login()
        {
            //ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
               var UM= db.Users.Where(u=>u.EmailID==user.EmailID && u.Password==user.Password);      
                if(UM.Count()>=0)
                {
                    SessionPersister.EmailID = user.EmailID;
                    return RedirectToAction("Index", "Home");                    
                }

            }        
            return View();
        }

        public ActionResult Logout()
        {            
            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();
            SessionPersister.EmailID = null;
            return RedirectToAction("Login", "Users");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", user.CityID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Name,EmailID,ContactNo,CountryID,CityID,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", user.CityID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
