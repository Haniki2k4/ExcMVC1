using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcMVC1.Models;

namespace ExcMVC1.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult ListCustomer()
        {
            // lấy dsach dữ liệu trong bảng
            TestWebEntities2 db = new TestWebEntities2();
            List<KhachHang> DsKHang = db.KhachHangs.ToList();
            return View(DsKHang);
        }

        public ActionResult AddKhachHang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddKhachHang(KhachHang model)
        {
            if (model.Name == "")
            {
                ModelState.AddModelError("", "Bạn cần nhập tên khách hàng.");
                return View(model);
            }
            if (model.ID <= 0 )
            {
                ModelState.AddModelError("", "Bạn cần nhập ID lớn hơn 0.");
                return View(model);
            }
            TestWebEntities2 db = new TestWebEntities2();
            if (db.KhachHangs.Any(m => m.ID == model.ID))
            {
                ModelState.AddModelError("", "ID đã tồn tại. Hãy chọn một ID khác.");
                return View(model);
            }
            db.KhachHangs.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListCustomer");
        }

        public ActionResult EditCustomer(int idKH)
        {
            TestWebEntities2 db = new TestWebEntities2();
            KhachHang model = db.KhachHangs.SingleOrDefault(m => m.ID == idKH);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCustomer(KhachHang model)
        {
            TestWebEntities2 db = new TestWebEntities2();
            var updKKH = db.KhachHangs.Find(model.ID);
            updKKH.SoDT = model.SoDT;
            updKKH.DiaChi = model.DiaChi;
            updKKH.Name = model.Name;
            updKKH.Email = model.Email;
            updKKH.GioiTinh = model.GioiTinh;
            db.SaveChanges();
            return RedirectToAction("ListCustomer");
        }

        public ActionResult DeleteCustomer(int idKH)
        {
            TestWebEntities2 db = new TestWebEntities2();
            var delModel = db.KhachHangs.Find(idKH);
            db.KhachHangs.Remove(delModel);
            db.SaveChanges();

            return RedirectToAction("ListCustomer");
        }
    }
}