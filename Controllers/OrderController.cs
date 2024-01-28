using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcMVC1.Models;

namespace ExcMVC1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult ListOrder()
        {
            TestWebEntities2 db = new TestWebEntities2();
            List<DonHang> DsDHang = db.DonHangs.ToList();
            return View(DsDHang);
        }
        public ActionResult AddDonHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDonHang(DonHang model)
        {
            if (model.Name == "")
            {
                ModelState.AddModelError("", "Bạn cần nhập tên khách hàng.");
                return View(model);
            }
            TestWebEntities2 db = new TestWebEntities2();
            db.DonHangs.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListOrder");
        }

        public ActionResult EditDonHang(int idDH)
        {
            TestWebEntities2 db = new TestWebEntities2();
            DonHang model = db.DonHangs.SingleOrDefault(m => m.ID == idDH);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditDonHang(DonHang model)
        {
            TestWebEntities2 db = new TestWebEntities2();
            var updDH = db.DonHangs.Find(model.ID);
            updDH.Name = model.Name;
            updDH.SDT = model.SDT;
            updDH.DiaChi = model.DiaChi;
            updDH.DateOrder = model.DateOrder;
            updDH.TenMay = model.TenMay;
            db.SaveChanges();
            return RedirectToAction("ListOrder");
        }

        public ActionResult DeleteDonHang(int idDH)
        {
            TestWebEntities2 db = new TestWebEntities2();
            var delModel = db.DonHangs.Find(idDH);
            db.DonHangs.Remove(delModel);
            db.SaveChanges();

            return RedirectToAction("ListOrder");
        }
    }
}