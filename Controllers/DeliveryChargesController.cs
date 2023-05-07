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
    public class DeliveryChargesController : Controller
    {
        private Model1 db = new Model1();

        // GET: DeliveryCharges
        public ActionResult Index()
        {
            return View(db.DeliveryCharges.ToList());
        }

        // GET: DeliveryCharges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryCharge deliveryCharge = db.DeliveryCharges.Find(id);
            if (deliveryCharge == null)
            {
                return HttpNotFound();
            }
            return View(deliveryCharge);
        }

        // GET: DeliveryCharges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryCharges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,value")] DeliveryCharge deliveryCharge)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryCharges.Add(deliveryCharge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryCharge);
        }

        // GET: DeliveryCharges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryCharge deliveryCharge = db.DeliveryCharges.Find(id);
            if (deliveryCharge == null)
            {
                return HttpNotFound();
            }
            return View(deliveryCharge);
        }

        // POST: DeliveryCharges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,value")] DeliveryCharge deliveryCharge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryCharge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryCharge);
        }

        // GET: DeliveryCharges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryCharge deliveryCharge = db.DeliveryCharges.Find(id);
            if (deliveryCharge == null)
            {
                return HttpNotFound();
            }
            return View(deliveryCharge);
        }

        // POST: DeliveryCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryCharge deliveryCharge = db.DeliveryCharges.Find(id);
            db.DeliveryCharges.Remove(deliveryCharge);
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
