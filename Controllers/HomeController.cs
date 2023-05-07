using SwissMetroCookware.Models;
using System;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Mail;

namespace SwissMetroCookware.Controllers
{
    public class HomeController : Controller
    {

        Model1 db = new Model1();

        public ActionResult IndexAdmin()
        {
            ViewBag.msgs = db.Messages.Where(x=>x.status == "Unread").Count();
            ViewBag.new_orders = db.Orders.Where(x => x.status == "Processing").Count();
            ViewBag.customers = db.Customers.Count();
            ViewBag.shipped_orders = db.Messages.Where(x => x.status == "Shipped").Count();
            ViewBag.delivered = db.Orders.Where(x => x.status == "Delivered").Count();
            ViewBag.feedbacks = db.Feedbacks.Count();
            ViewBag.products = db.Products.Count();
            ViewBag.total_orders = db.Orders.Count();
            ViewBag.employees = db.Employees.Count();
            ViewBag.contacts = db.Phonebooks.Count();
            ViewBag.logistics = db.Logistics.Count();
            ViewBag.facebook = db.Miscs.Where(x=>x.id == 3).FirstOrDefault().description;
            ViewBag.twitter = db.Miscs.Where(x => x.id == 4).FirstOrDefault().description;
            ViewBag.linkedin = db.Miscs.Where(x => x.id == 9).FirstOrDefault().description;
            ViewBag.googleplus = db.Miscs.Where(x => x.id == 8).FirstOrDefault().description;
            ViewBag.n_orders = db.Orders.Where(x => x.status == "Processing").ToList();

            return View();
        }

        public ActionResult IndexManager()
        {
            ViewBag.msgs = db.Messages.Where(x => x.status == "Unread").Count();
            ViewBag.new_orders = db.Orders.Where(x => x.status == "Processing").Count();
            ViewBag.customers = db.Customers.Count();
            ViewBag.shipped_orders = db.Messages.Where(x => x.status == "Shipped").Count();
            ViewBag.delivered = db.Orders.Where(x => x.status == "Delivered").Count();
            ViewBag.feedbacks = db.Feedbacks.Count();
            ViewBag.products = db.Products.Count();
            ViewBag.total_orders = db.Orders.Count();
            ViewBag.employees = db.Employees.Count();
            ViewBag.contacts = db.Phonebooks.Count();
            ViewBag.logistics = db.Logistics.Count();
            ViewBag.facebook = db.Miscs.Where(x => x.id == 3).FirstOrDefault().description;
            ViewBag.twitter = db.Miscs.Where(x => x.id == 4).FirstOrDefault().description;
            ViewBag.linkedin = db.Miscs.Where(x => x.id == 9).FirstOrDefault().description;
            ViewBag.googleplus = db.Miscs.Where(x => x.id == 8).FirstOrDefault().description;
            ViewBag.n_orders = db.Orders.Where(x => x.status == "Processing").ToList();

            return View();
        }


        public ActionResult EditLinks()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLinks(Misc m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditLinks");
            }
            return View();
        }
        public ActionResult IndexCustomer()
        {
            List<SubCategory> sub = db.SubCategories.ToList();
            List<Product> sale = db.Products.Where(x=> x.sale_percent != "0" && x.sale_percent != null).ToList();
            List<Product> featured = db.Products.Where(y => y.featured_product == "Yes").ToList();
            ViewBag.sub = sub;
            ViewBag.sale = sale;
            ViewBag.featured = featured;
            return View();
        }


        public ActionResult Terms()
        {
            ShopModel s = new ShopModel();
            s.Cat = db.Categories.ToList();
            s.Sub = db.SubCategories.ToList();
            s.Pro = db.Products.ToList();
            return View(s);
            
        }
        public ActionResult Guide()
        {
            ShopModel s = new ShopModel();
            s.Cat = db.Categories.ToList();
            s.Sub = db.SubCategories.ToList();
            s.Pro = db.Products.ToList();
            return View(s);

        }

        public ActionResult PrivacyPolicy()
        {
            ShopModel s = new ShopModel();
            s.Cat = db.Categories.ToList();
            s.Sub = db.SubCategories.ToList();
            s.Pro = db.Products.ToList();
            return View(s);

        }

        public ActionResult MyAccount(int id)
        {
            Customer c = db.Customers.Where(x => x.id == id).FirstOrDefault();
            List<Order> od = db.Orders.Where(x => x.Customer.id == id && x.status != "Cancelled").ToList();
            List<Order> cod = db.Orders.Where(x => x.Customer.id == id && x.status == "Cancelled").ToList();
            List<OrderDetail> fp_orders = db.OrderDetails.Where(x => x.Order.status == "Delivered" && x.Order.customer_fid == c.id).ToList();
            List<Feedback> my_feedbacks = db.Feedbacks.Where(x => x.customer_fid == c.id).ToList();
            ViewBag.orders = od;
            ViewBag.customer = c;
            ViewBag.c_orders = cod;
            ViewBag.fp_orders = fp_orders;
            ViewBag.my_feedbacks = my_feedbacks;
            return View(c);
        }

        public ActionResult ProvideFeedback(int id)
        {
            OrderDetail od = db.OrderDetails.Where(x => x.id == id).FirstOrDefault();
            ViewBag.od = od;
            return View();
        }


        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult CalendarManager()
        {
            return View();
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }
        public ActionResult LogoutAdmin()
        {
            Session["Admin"] = null;
            return RedirectToAction("LoginAdmin" , "Home");
        }
        public ActionResult LogoutManager()
        {
            Session["Manager"] = null;
            return RedirectToAction("LoginManager", "Home");
        }

        public ActionResult LoginManager()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(Admin a)
        {
            Admin admin = db.Admins.Where(x => x.email == a.email && x.password == a.password).FirstOrDefault();
            if (admin != null)
            {
                Session["Admin"] = admin;
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }

        [HttpPost]
        public ActionResult LoginManager(Manager m)
        {
            Manager manager = db.Managers.Where(x => x.email == m.email && x.password == m.password).FirstOrDefault();
            if (manager != null)
            {
                Session["Manager"] = manager;
                return RedirectToAction("IndexManager", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Checkout(int total)
        {
            List<Product> list;
            if (Session["Customer"] == null)
            {
                return RedirectToAction("LoginCustomer", "Customers");
            }
            else
            {
                list = (List<Product>)Session["myCart"];
            }
            DeliveryCharge dc = db.DeliveryCharges.Where(x => x.name == "Punjab").FirstOrDefault();
            ViewBag.punjab = dc.value;
            DeliveryCharge dc2 = db.DeliveryCharges.Where(x => x.name == "Sindh").FirstOrDefault();
            ViewBag.sindh = dc2.value;
            DeliveryCharge dc3 = db.DeliveryCharges.Where(x => x.name == "KPK").FirstOrDefault();
            ViewBag.kpk = dc3.value;
            DeliveryCharge dc4 = db.DeliveryCharges.Where(x => x.name == "Balochistan").FirstOrDefault();
            ViewBag.b = dc4.value;
            DeliveryCharge dc5 = db.DeliveryCharges.Where(x => x.name == "Gilgit Baltistan").FirstOrDefault();
            ViewBag.gb = dc5.value;

            ViewBag.cart = list;
            ViewBag.total = total;
            return View();
        }

     
        [HttpPost]
        public ActionResult PayNow(Order o)
        {
            o.date = System.DateTime.Now;
            Customer c = (Customer)Session["Customer"];
            o.customer_fid = c.id;
            o.status = "Processing";
            Session["Order"] = o;
            if (o.payment_type == "PayPal")
            {
                return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=swissmetrocookware@gmail.com&item_name=SwissMEtroProducts&return=https://localhost:44349/Home/OrderBooked&amount="+(o.amount/180));
            }
            else
            {
                return RedirectToAction("OrderBooked", "Home");
            }
        }


        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("bit18-001@pugc.edu.pk");
            mail.To.Add(o.email);
            mail.Subject = "Order Confirmation";
            mail.Body = "<b>Swiss Metro Cookware</b><br/> Thanks for placing order. <br/> Please visit our website <a href=\"www.swissmetrocookware.com\">Swiss Metro Cookware</a> for more information.";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("bit18-001@pugc.edu.pk", "pugc232316");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            //////Code for sms sending
            //String api = "https://lifetimesms.com/json?api_token=aba4644521f2df3754dc86bd50e8f8dede47627268&api_secret=SMC&to=?" + o.contact + "&from=SwissMetroCookware&message=Order Confirmation! Thank you for placing order.Please visit our website for more details!";
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);
            //var httpResponse = (HttpWebResponse)req.GetResponse();


            //code for saving data in database

            db.Orders.Add(o);
           
            db.SaveChanges();
            List<Product> p = (List<Product>)Session["mycart"];
            for (int i = 0; i < p.Count(); i++)
            {
                OrderDetail od = new OrderDetail();
                int o_id = db.Orders.Max(x => x.id);
                od.order_fid = o_id;
                od.product_fid = p[i].id;
                od.quantity = p[i].quan;
                od.cost_price = p[i].cost_price;
                if (p[i].sale_percent != "0" && p[i].sale_percent != null)
                {
                    int price = Convert.ToInt32(p[i].sale_price);
                    int percent = Convert.ToInt32(p[i].sale_percent);
                    int discount_price = (price * percent) / 100;
                    int new_price = price - discount_price;
                    od.sale_price = new_price;
                }
                else
                {
                    od.sale_price = p[i].sale_price;
                }
                MinusFromStock(p[i].id , p[i].quan);
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }
            ViewBag.order = o;
            Session["Order"] = null;
            Session["myCart"] = null;
            return View();
        }

        public void MinusFromStock(int id , int quantity)
        {
            Product p = db.Products.Where(x => x.id == id).FirstOrDefault();
            p.quantity = p.quantity-quantity;
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return;
        }

        public void AddToStock(int id, int quantity)
        {
            Product p = db.Products.Where(x => x.id == id).FirstOrDefault();
            p.quantity = p.quantity + quantity;
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return;
        }
        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Invoice(int id)
        {
            Order o;
            o = db.Orders.Where(x => x.id == id).FirstOrDefault();
            List<OrderDetail> od_list;
            od_list = db.OrderDetails.Where(x => x.order_fid == id).ToList();
            ViewBag.od_list = od_list;
            ViewBag.o = o;
            return View();
        }

        public ActionResult CancelOrder(int id)
        {
            Order o;
            o = db.Orders.Where(x => x.id == id).FirstOrDefault();
            o.status = "Cancellation Pending";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            Response.Redirect(Request.UrlReferrer.ToString());
            return View();
        }


        public ActionResult ViewOrderDetails(int id)
        {
            Order o = db.Orders.Where(x => x.id == id).FirstOrDefault();

            ViewBag.order = o;
            return View();
        }

        public ActionResult Subscribe()
        {
            return View();
        }


        public ActionResult ReviewCancellations()
        {
            List<Order> o = db.Orders.Where(x => x.status == "Cancellation Pending").ToList();
            ViewBag.o = o;
            return View();
        }

        public ActionResult ReviewOrders()
        {
            List<Order> o = db.Orders.Where(x => x.status == "Processing").ToList();
            ViewBag.o = o;
            return View();
        }

        public ActionResult ConfirmCancellaion(int id)
        {
            Order o = db.Orders.Where(x => x.id == id).FirstOrDefault();
            o.status = "Cancelled";
            List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == o.id).ToList();
            foreach (var item in od)
            {
                AddToStock(item.Product.id, item.quantity);
            }
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ReviewCancellations", "Home");
        }

        public ActionResult ConfirmOrder(int id)
        {
            Order o = db.Orders.Where(x => x.id == id).FirstOrDefault();
            o.status = "Booked";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ReviewOrders", "Home");
        }
        public ActionResult AddToCart(int id)
        {
            List<Product> list;
            if (Session["myCart"] == null)
            {
                list = new List<Product>();
            }
            else
            {
                int RowNo = 0;
                list = (List<Product>)Session["myCart"];
                foreach (var item in list)
                {
                    if (item.id == id)
                    {
                        PlusToCart(RowNo);
                        return RedirectToAction("Cart");
                    }
                    RowNo++;
                }
            }
            list.Add(db.Products.Where(p => p.id == id).FirstOrDefault());
            list[list.Count - 1].quan = 1;
            Session["myCart"] = list; //saved in session state.. can be used to store data in the form of a table, list, actionresult, and primitive types too.. because this list will be used on multiple pages, like car, payment, checkout etc...
            return RedirectToAction("Cart");
        }


        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list.RemoveAt(RowNo);
            Session["myCart"] = list; //saved in session state.. can be used to store data in the form of a table, list, actionresult, and primitive types too.. because this list will be used on multiple pages, like car, payment, checkout etc...
            return RedirectToAction("Cart");
        }
        public ActionResult ClearCart()
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list.Clear();
            Session["myCart"] = null; //saved in session state.. can be used to store data in the form of a table, list, actionresult, and primitive types too.. because this list will be used on multiple pages, like car, payment, checkout etc...
            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].quan++;
            if(list[RowNo].quan > list[RowNo].quantity)
            {
                ViewBag.Stock = "Only " + list[RowNo].quantity + " item left in stock";
                list[RowNo].quan--;
                Session["myCart"] = list;
                return RedirectToAction("Cart");
            }
            Session["myCart"] = list; //saved in session state.. can be used to store data in the form of a table, list, actionresult, and primitive types too.. because this list will be used on multiple pages, like car, payment, checkout etc...
            return RedirectToAction("Cart");
        }
        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].quan--;
            if (list[RowNo].quan <= 0)
            { 
                RemoveFromCart(RowNo);
            }
            Session["myCart"] = list; //saved in session state.. can be used to store data in the form of a table, list, actionresult, and primitive types too.. because this list will be used on multiple pages, like car, payment, checkout etc...
            return RedirectToAction("Cart");
        }
        public ActionResult Shop(int? id)
        {
            ShopModel s = new ShopModel();
            s.Cat = db.Categories.ToList();
            s.Sub = db.SubCategories.ToList();
            if (id == null)
                s.Pro = db.Products.ToList();
            else
                s.Pro = db.Products.Where(p => p.sub_category_fid == id).ToList();
            return View(s);
        }

        public ActionResult EditSale()
        {
            ViewBag.sale = db.Sales.Where(x=>x.id == 1).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditSale(Sale s)
        {
           
            db.Entry(s).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditSale", "Home");
        }

        public ActionResult SaleProducts()
        {
            return View();
        }
        public ActionResult SingleProduct(int id)
        {

            Product p = db.Products.Where(x => x.id == id).FirstOrDefault();
            ViewBag.product = p;
            List<Product> list = db.Products.Where(x => x.SubCategory.id == p.SubCategory.id).ToList();
            ViewBag.related = list;
            return View();
        }

        [HttpPost]
        public ActionResult AssignLogistics(Order o)
        {
            Order order = db.Orders.Where(x => x.id == o.id).FirstOrDefault();
            order.logistic = o.logistic;
            order.status = "Shipped";
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ToBeShipped", "Home");
        }

        public ActionResult ToBeShipped()
        {
            List<Order> list = db.Orders.Where(x => x.status == "Booked").ToList();
            ViewBag.o = list;
            return View();
        }

        public ActionResult ToBeDelivered()
        {
            List<Order> list = db.Orders.Where(x => x.status == "Shipped").ToList();
            ViewBag.o = list;
            return View();
        }

        public ActionResult Delivered(int id)
        {
            Order o = db.Orders.Where(x => x.id == id).FirstOrDefault();
            o.status = "Delivered";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ToBeDelivered", "Home");
        }

        public ActionResult Feedback()
        {
            return View();
        }

        public ActionResult NewMessages()
        {
            List<Message> msg = db.Messages.Where(x => x.status == "Unread").ToList();
            ViewBag.msgs = msg;
            return View();
        }

        public ActionResult ReadMessages()
        {
            List<Message> msg = db.Messages.Where(x => x.status == "Read").ToList();
            ViewBag.msgs = msg;
            return View();
        }

        public ActionResult MarkAsRead(int id)
        {
            Message m = db.Messages.Where(x=>x.id == id).FirstOrDefault();
            m.status = "Read";
            db.Entry(m).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("NewMessages" , "Home");
        }
        public ActionResult Wishlist()
        {
            if (Session["Customer"] != null)
            {
                Customer c = (Customer)Session["Customer"];
                List<Wishlist> list = db.Wishlists.Where(x => x.customer_fid == c.id).ToList();
                ViewBag.wishlist = list;
                return View();
            }
            else
            {
                return RedirectToAction("LoginCustomer" , "Customers");
            }
        }
        public ActionResult AddToWishlist(int id)
        {
            if (Session["Customer"] != null)
            {
                Customer c = (Customer)Session["Customer"];
                Wishlist wish = new Wishlist();
                if (Session["wish"] == null)
                {
                    wish.product_fid = id;
                }
                else
                {
                    wish.product_fid = (int)Session["wish"];
                }
                wish.customer_fid = c.id; 
                db.Wishlists.Add(wish);
                db.SaveChanges();
                return RedirectToAction("Wishlist", "Home");
            }
            else
            {
                Session["wish"] = id;
                return RedirectToAction("LoginCustomer", "Customers");
            }
        }

        public ActionResult DeleteFromWishlist(int id)
        {
            Product p = db.Products.Where(x => x.id == id).FirstOrDefault();
            Wishlist wish = db.Wishlists.Where(y=>y.Product.id == p.id).FirstOrDefault();
      
            db.Wishlists.Remove(wish);
            db.SaveChanges();
            return RedirectToAction("Wishlist" , "Home");
        }
        public ActionResult JazzCashForm()
        {
            return View();
        }
    }
}