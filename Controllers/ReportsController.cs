using SwissMetroCookware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;


namespace SwissMetroCookware.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        Model1 db = new Model1();
        public ActionResult SaleReport(FilterModel fm)
        {
            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            { ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s"); }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            { ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s"); }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name }).ToList();

            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name + " (" + x.description + ")" }).ToList();
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.SubCategory.category_fid == fm.Category).Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name + " (" + x.description + ")" }).ToList();
            }

            var od = db.OrderDetails.Select(x => x.order_fid).ToList();

            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.SubCategory.category_fid == fm.Category).Select(x => x.id).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.id == fm.Product).Select(x => x.id).ToList();
                }
                od = db.OrderDetails.Where(x => p.Contains(x.product_fid)).Select(x => x.order_fid).ToList();
            }

            var o = db.OrderDetails.Where(x =>  x.Order.date >= fm.DateFrom & x.Order.date <= fm.DateTo & od.Contains(x.Order.id)).OrderByDescending(x => x.Order.date).ToList();
            return View(o);
        }
        public ActionResult ProfitandLossReport(FilterModel fm)
        {
            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            { ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s"); }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            { ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s"); }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.name.ToString(), Text = x.name }).ToList();

            if (fm.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name + " (" + x.description + ")" }).ToList();
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.SubCategory.category_fid == fm.Category).Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name + " (" + x.description + ")" }).ToList();
            }

            var od = db.OrderDetails.Select(x => x.order_fid).ToList();

            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.SubCategory.category_fid == fm.Category).Select(x => x.id).ToList();
                if (fm.Product != null)
                {
                    p = db.Products.Where(x => x.id == fm.Product).Select(x => x.id).ToList();
                }
                od = db.OrderDetails.Where(x => p.Contains(x.id)).Select(x => x.order_fid).ToList();
            }

            var o = db.Orders.Where(x => x.date >= fm.DateFrom & x.date <= fm.DateTo & od.Contains(x.id)).OrderByDescending(x => x.date).ToList();
            return View(o);
        }
       
        public ActionResult StockReport()
        {

            var p = db.Products.ToList();

          
            return View(p);
        }
        public ActionResult Invoice(int id)
        {
            List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == id).ToList();
            ViewBag.od = od;
            ViewBag.o = db.Orders.Where(x => x.id == id).FirstOrDefault();

            return View();
        }

        public ActionResult DailySaleReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int day = moment.Day;
            int month = moment.Month;
            int year = moment.Year;
            List<Order> orders = db.Orders.Where(x => x.date.Day == day && x.date.Month == month && x.date.Year == year && x.status != "Cancelled" && x.status != "Cancellation Pending").ToList();
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.products = db.Products.ToList();

            return View();
        }

        public ActionResult MonthlySaleReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int month = moment.Month;
            int year = moment.Year;
            List<Order> orders = db.Orders.Where(x => x.date.Month == month && x.date.Year == year && x.status != "Cancelled" && x.status != "Cancellation Pending").ToList();
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.month = moment.ToString("MMMM");
            ViewBag.year = year;
            ViewBag.products = db.Products.ToList();
            return View();
        }

        public ActionResult AnnualSaleReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int year = moment.Year;
            List<Order> orders = db.Orders.Where(x => x.date.Year == year && x.status != "Cancelled" && x.status != "Cancellation Pending").ToList();
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.year = year;
            ViewBag.products = db.Products.ToList();
            return View();
        }

        public ActionResult WeeklySaleReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int weekOfMonth = (moment.Day + ((int)moment.DayOfWeek)) / 7 + 1;
            int year = moment.Year;
            int month = moment.Month;
            List<Order> orders = new List<Order>();
            List<Order> o = db.Orders.Where(x => x.status != "Cancelled" && x.status != "CancellationPending").ToList();
            foreach (var item in o)
            {
                DateTime dt = item.date;
                int o_week = (dt.Day + ((int)dt.DayOfWeek)) / 7 + 1;
                int o_month = dt.Month;
                int o_year = dt.Year;
                if (o_week == weekOfMonth && o_month == month && o_year == year)
                {
                    orders.Add(item);
                }
            }
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.year = year;
            ViewBag.products = db.Products.ToList();
            return View();
        }

        public ActionResult DailyProfitLossReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int day = moment.Day;
            int month = moment.Month;
            int year = moment.Year;
            List<Order> orders = db.Orders.Where(x => x.date.Day == day && x.date.Month == month && x.date.Year == year && x.status != "Cancelled" && x.status != "Cancellation Pending").ToList();
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.products = db.Products.ToList();

            return View();
        }

        public ActionResult WeeklyProfitLossReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int month = moment.Month;
            int year = moment.Year;
            int weekOfMonth = (moment.Day + ((int)moment.DayOfWeek)) / 7 + 1;
            List<Order> orders = new List<Order>();
            List<Order> o = db.Orders.Where(x => x.status != "Cancelled" && x.status != "CancellationPending").ToList();
            foreach (var item in o)
            {
                DateTime dt = item.date;
                int o_week = (dt.Day + ((int)dt.DayOfWeek)) / 7 + 1;
                int o_month = dt.Month;
                int o_year = dt.Year;
                if (o_week == weekOfMonth && o_month == month && o_year == year)
                {
                    orders.Add(item);
                }
            }

            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.products = db.Products.ToList();

            return View();
        }


        public ActionResult MonthlyProfitLossReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int month = moment.Month;
            int year = moment.Year;
            List<Order> orders = db.Orders.Where(x => x.date.Month == month && x.date.Year == year && x.status != "Cancelled" && x.status != "Cancellation Pending").ToList();
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.month = moment.ToString("MMMM");
            ViewBag.year = year;
            ViewBag.products = db.Products.ToList();
            return View();
        }

        public ActionResult AnnualProfitLossReport()
        {
            System.DateTime moment = System.DateTime.Today;
            int year = moment.Year;
            List<Order> orders = db.Orders.Where(x => x.date.Year == year && x.status != "Cancelled" && x.status != "Cancellation Pending").ToList();
            List<OrderDetail> o_details = new List<OrderDetail>();
            foreach (var item in orders)
            {
                List<OrderDetail> od = db.OrderDetails.Where(x => x.order_fid == item.id).ToList();
                if (o_details == null)
                {
                    o_details = od;
                }
                else
                {
                    o_details.AddRange(od);
                }
            }
            ViewBag.orders = orders;
            ViewBag.od = o_details;
            ViewBag.year = year;
            ViewBag.products = db.Products.ToList();
            return View();
        }




    }
}