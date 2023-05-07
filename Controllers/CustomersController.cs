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
    public class CustomersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        public ActionResult LoginCustomer()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginCustomer(Customer customer)
        {
            Customer c = db.Customers.Where(x => x.email == customer.email && x.password == customer.password).FirstOrDefault();
            if (c != null)
            {
                Session["Customer"] = c;
                return RedirectToAction("Shop", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["Customer"] = null;
            return RedirectToAction("IndexCustomer", "Home");
        }

        public ActionResult ChangeAccount()
        {
            Session["Customer"] = null;
            return RedirectToAction("LoginCustomer", "Customers");
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.pic != null)
                {
                    customer.pic.SaveAs(Server.MapPath("~/CustomerPictures/" + customer.pic.FileName));
                    customer.picture = "~/CustomerPictures/" + customer.pic.FileName;
                }
                else if (customer.pic == null)
                {
                    if (customer.gender == "Male")
                    {

                        customer.picture = "~/CustomerPictures/male.jpg";
                    }
                    else if (customer.gender == "Female")
                    {

                        customer.picture = "~/CustomerPictures/female.jpg";
                    }
                }
                db.Customers.Add(customer);
                db.SaveChanges();
                Session["Customer"] = customer;
                return RedirectToAction("IndexCustomer" , "Home");
            }

            return RedirectToAction("LoginCustomer" , "Customers");
        }


        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,address,password,contact,province,picture,gender,dob")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexCustomer" , "Home");
            }
            return RedirectToAction("IndexCustomer" , "Home");
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
