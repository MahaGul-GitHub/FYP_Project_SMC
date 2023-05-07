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
    public class PhonebooksController : Controller
    {
        private Model1 db = new Model1();

        // GET: Phonebooks
        public ActionResult Index()
        {
            return View(db.Phonebooks.ToList());
        }

        // GET: Phonebooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonebook phonebook = db.Phonebooks.Find(id);
            if (phonebook == null)
            {
                return HttpNotFound();
            }
            return View(phonebook);
        }

        // GET: Phonebooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phonebooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,notes,phone,company")] Phonebook phonebook)
        {
            if (ModelState.IsValid)
            {
                db.Phonebooks.Add(phonebook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phonebook);
        }

        // GET: Phonebooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonebook phonebook = db.Phonebooks.Find(id);
            if (phonebook == null)
            {
                return HttpNotFound();
            }
            return View(phonebook);
        }

        // POST: Phonebooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,notes,phone,company")] Phonebook phonebook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phonebook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phonebook);
        }

        // GET: Phonebooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phonebook phonebook = db.Phonebooks.Find(id);
            if (phonebook == null)
            {
                return HttpNotFound();
            }
            return View(phonebook);
        }

        // POST: Phonebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phonebook phonebook = db.Phonebooks.Find(id);
            db.Phonebooks.Remove(phonebook);
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
