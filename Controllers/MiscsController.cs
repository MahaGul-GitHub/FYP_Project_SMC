using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SwissMetroCookware.Models;

namespace SwissMetroCookware.Controllers
{
    public class MiscsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Miscs
        public ActionResult Index()
        {
            return View(db.Miscs.ToList());
        }

        // GET: Miscs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Misc misc = db.Miscs.Find(id);
            if (misc == null)
            {
                return HttpNotFound();
            }
            return View(misc);
        }

        // GET: Miscs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Miscs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,year,description,amount")] Misc misc)
        {
            if (ModelState.IsValid)
            {
                db.Miscs.Add(misc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(misc);
        }

        // GET: Miscs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Misc misc = db.Miscs.Find(id);
            if (misc == null)
            {
                return HttpNotFound();
            }
            return View(misc);
        }

        // POST: Miscs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Misc misc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(misc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(misc);
        }

        // GET: Miscs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Misc misc = db.Miscs.Find(id);
            if (misc == null)
            {
                return HttpNotFound();
            }
            return View(misc);
        }

        // POST: Miscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Misc misc = db.Miscs.Find(id);
            db.Miscs.Remove(misc);
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
