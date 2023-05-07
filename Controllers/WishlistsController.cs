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
    public class WishlistsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Wishlists
        public ActionResult Index()
        {
            var wishlists = db.Wishlists.Include(w => w.Customer).Include(w => w.Product);
            return View(wishlists.ToList());
        }

        // GET: Wishlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // GET: Wishlists/Create
        public ActionResult Create()
        {
            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name");
            ViewBag.product_fid = new SelectList(db.Products, "id", "name");
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,customer_fid,product_fid")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.Wishlists.Add(wishlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name", wishlist.customer_fid);
            ViewBag.product_fid = new SelectList(db.Products, "id", "name", wishlist.product_fid);
            return View(wishlist);
        }

        // GET: Wishlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name", wishlist.customer_fid);
            ViewBag.product_fid = new SelectList(db.Products, "id", "name", wishlist.product_fid);
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customer_fid,product_fid")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_fid = new SelectList(db.Customers, "id", "name", wishlist.customer_fid);
            ViewBag.product_fid = new SelectList(db.Products, "id", "name", wishlist.product_fid);
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wishlist wishlist = db.Wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: Wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wishlist wishlist = db.Wishlists.Find(id);
            db.Wishlists.Remove(wishlist);
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
