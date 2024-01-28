using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcMVC1.Models;

namespace ExcMVC1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult ListMayTinh()
        {
            TestWebEntities2 db = new TestWebEntities2();
            List<MayTinh> DsMtinh = db.MayTinhs.ToList();
            return View(DsMtinh);
        }

        public ActionResult AddMaytinh()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMaytinh(MayTinh model)
        {
            if (model.GiaBan <= 0)
            {
                ModelState.AddModelError("", "Bạn cần nhập giá bán lớn hơn 0.");
                return View(model);
            }
            if (ModelState.IsValid == true)
            {
                TestWebEntities2 db = new TestWebEntities2();
                db.MayTinhs.Add(model);
                db.SaveChanges();
                return RedirectToAction("ListMayTinh");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Input");
                return View(model);
            }
        }


        public ActionResult EditMaytinh(string idMT)
        {
            TestWebEntities2 db = new TestWebEntities2();
            MayTinh model = db.MayTinhs.SingleOrDefault(m => m.MaMay == idMT);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditMaytinh(MayTinh model)
        {
            if (ModelState.IsValid == true)
            {
                if (model.GiaBan <= 0)
                {
                    ModelState.AddModelError("", "Bạn cần nhập giá bán lớn hơn 0.");
                    return View(model);
                }
                TestWebEntities2 db = new TestWebEntities2();
                var updMT = db.MayTinhs.Find(model.MaMay);
                updMT.DongMay = model.DongMay;
                updMT.GiaBan = model.GiaBan;
                updMT.NgaySX = model.NgaySX;
                updMT.HangSX = model.HangSX;
                db.SaveChanges();
                return RedirectToAction("ListMayTinh");
            }
            else
            {
                //kem theo 1 tbao
                ModelState.AddModelError("", "Invalid Input");

                return View(model);
            }
        }


        [HttpGet]
        public ActionResult DeleteMayTinh(string idMT)
        {
            TestWebEntities2 db = new TestWebEntities2();
            var delModel = db.MayTinhs.Find(idMT);
            db.MayTinhs.Remove(delModel);
            db.SaveChanges();

            return RedirectToAction("ListMayTinh");
        }


    }
}