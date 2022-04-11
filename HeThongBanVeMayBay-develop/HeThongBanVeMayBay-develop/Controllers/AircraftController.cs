using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true, diffe")]
    public class AircraftController : Controller
    {
        // GET: Aircraft
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<HANGBAY> SelectAllArticle1()
        {
            var rtn = new List<HANGBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.HANGBAYs)
                {
                    rtn.Add(new HANGBAY
                    {
                        IDHangBay = item.IDHangBay,
                        TenHangbay = item.TenHangbay,
                        ImageAviation = item.ImageAviation
                    });
                }
            }
            return rtn;
        }

        public ActionResult Index()
        {
            var username = User.Identity.Name;
            string data = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
            if ((database.CHUCVUs.Where(x => x.IDChucVu.Trim() == data).FirstOrDefault().IsAdmin) == "true")
            {
                return View(database.MAYBAYs.ToList());
            }  
            else
            {
                return View(database.MAYBAYs.Where(s => s.HangBay.Trim() == database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim()));
            }    
        }

        public ActionResult Create()
        {
            var username = User.Identity.Name;
            string data = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
            if ((database.CHUCVUs.Where(x => x.IDChucVu.Trim() == data).FirstOrDefault().IsAdmin) == "true")
            {
                List<HANGBAY> list = SelectAllArticle1().ToList();
                ViewBag.listHangBay = new SelectList(list, "IDHangBay", "TenHangBay", "");
                MAYBAY mb = new MAYBAY();
                return View(mb);
            }    
            else
            {
                string data1 = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
                List<HANGBAY> list = database.HANGBAYs.Where(s => s.IDHangBay == data1).ToList();
                ViewBag.listHangBay = new SelectList(list, "IDHangBay", "TenHangBay", "");
                MAYBAY mb = new MAYBAY();
                return View(mb);
            }    
        }

        [HttpPost]
        public ActionResult Create(MAYBAY mb)
        {
            List<HANGBAY> list = SelectAllArticle1().ToList();
            try
            {
                ViewBag.listHangBay = new SelectList(list, "IDHangBay", "TenHangBay", 1);
                database.MAYBAYs.Add(mb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        public ActionResult Details(int Id)
        {
            return View(database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            var username = User.Identity.Name;
            string data = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
            if ((database.CHUCVUs.Where(x => x.IDChucVu.Trim() == data).FirstOrDefault().IsAdmin) == "true")
            {
                List<HANGBAY> list = SelectAllArticle1().ToList();
                ViewBag.listHangBay = new SelectList(list, "IDHangBay", "TenHangBay", "");
            }
            else
            {
                string data1 = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
                List<HANGBAY> list = database.HANGBAYs.Where(s => s.IDHangBay == data1).ToList();
                ViewBag.listHangBay = new SelectList(list, "IDHangBay", "TenHangBay", "");
            }
            return View(database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, MAYBAY mb)
        {
            List<HANGBAY> list = SelectAllArticle1().ToList();
            ViewBag.listHangBay = new SelectList(list, "IDHangBay", "TenHangBay", 1);
            database.Entry(mb).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, MAYBAY mb)
        {
            try
            {
                mb = database.MAYBAYs.Where(s => s.ID == Id).FirstOrDefault();
                database.MAYBAYs.Remove(mb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Aircraft");
            }
        }
    }
}