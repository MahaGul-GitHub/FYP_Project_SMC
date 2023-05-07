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
    public class FeedbacksController : Controller
    {
        private Model1 db = new Model1();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedbacks = db.Feedbacks.Include(f => f.Customer).Include(f => f.Product);
            return View(feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name");
            ViewBag.product_fid = new SelectList(db.Products, "id", "name");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,feedback1,customer_fid,product_fid,rating")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
              
            }

            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name", feedback.customer_fid);
            ViewBag.product_fid = new SelectList(db.Products, "id", "name", feedback.product_fid);
            return RedirectToAction("MyAccount", "Home", new { id = feedback.customer_fid});
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name", feedback.customer_fid);
            ViewBag.product_fid = new SelectList(db.Products, "id", "name", feedback.product_fid);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,feedback1,customer_fid,product_fid,rating")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name", feedback.customer_fid);
            ViewBag.product_fid = new SelectList(db.Products, "id", "name", feedback.product_fid);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
