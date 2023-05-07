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
    public class SubCategoriesController : Controller
    {
        private Model1 db = new Model1();

        // GET: SubCategories
        public ActionResult Index()
        {
            var subCategories = db.SubCategories.Include(s => s.Category);
            return View(subCategories.ToList());
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            ViewBag.category_fid = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.pic.SaveAs(Server.MapPath("~/SubCategoryPictures/" + subCategory.pic.FileName));
                subCategory.picture = "~/SubCategoryPictures/" + subCategory.pic.FileName;

                db.SubCategories.Add(subCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_fid = new SelectList(db.Categories, "id", "name", subCategory.category_fid);
            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_fid = new SelectList(db.Categories, "id", "name", subCategory.category_fid);
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCategory subCategory)
        {
            
            if (ModelState.IsValid)
            {
                if (subCategory.pic != null)
                {

                    subCategory.pic.SaveAs(Server.MapPath("~/SubCategoryPictures/" + subCategory.pic.FileName));
                    subCategory.picture = "~/SubCategoryPictures/" + subCategory.pic.FileName;
                }

                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_fid = new SelectList(db.Categories, "id", "name", subCategory.category_fid);
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
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
