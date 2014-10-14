using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WelcomeMVC.Models;

namespace WelcomeMVC.Controllers
{
    public class GuestBookController : Controller
    {
        private GuestBookEntities db = new GuestBookEntities();

        //
        // GET: /GuestBook/

        public ActionResult Index()
        {
            return View(db.GB.ToList());
        }

        public ActionResult List() 
        {
            return View("_List",db.GB.ToList());
        }

        //
        // GET: /GuestBook/Details/5

        public ActionResult Details(Guid id)
        {
            GB gb = db.GB.Find(id);
            if (gb == null)
            {
                return HttpNotFound();
            }
            return View(gb);
        }

        //
        // GET: /GuestBook/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GuestBook/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GB gb)
        {
            if (ModelState.IsValid)
            {
                gb.ID = Guid.NewGuid();
                gb.PostTime = DateTime.Now;
                db.GB.Add(gb);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(gb);
        }

        //
        // GET: /GuestBook/Edit/5

        public ActionResult Edit(Guid id)
        {
            GB gb = db.GB.Find(id);
            if (gb == null)
            {
                return HttpNotFound();
            }
            return View(gb);
        }

        //
        // POST: /GuestBook/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GB gb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gb);
        }

        //
        // GET: /GuestBook/Delete/5

        public ActionResult Delete(Guid id)
        {
            GB gb = db.GB.Find(id);
            if (gb == null)
            {
                return HttpNotFound();
            }
            return View(gb);
        }

        //
        // POST: /GuestBook/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GB gb = db.GB.Find(id);
            db.GB.Remove(gb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}