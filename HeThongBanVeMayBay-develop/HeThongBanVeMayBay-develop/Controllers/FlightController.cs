using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true, diffe")]
    public class FlightController : Controller
    {
        // GET: Flight
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<CHUYENBAY> SelectAllArticle()
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs)
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,                        
                        IDChuyenBay = item.IDChuyenBay,
                        HangBay = item.HANGBAY1.TenHangbay,
                        IDSanBayDen = item.SANBAY1.TenSB,
                        IDSanBayDi = item.SANBAY.TenSB,
                        GiaTien = item.GiaTien,
                        NgayBay = item.NgayBay.Date,
                        GioBay = item.GioBay,
                        ThoiGianToiDuKien = item.ThoiGianToiDuKien,
                        SoGheHang1 = item.SoGheHang1,
                        SoGheHang2 = item.SoGheHang2
                    });
                }
            }
            return rtn;
        }
        public static List<SANBAY> SelectAllArticle1()
        {
            var rtn = new List<SANBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.SANBAYs)
                {
                    rtn.Add(new SANBAY
                    {
                        IATA = item.IATA,
                        TenSB = item.TenSB
                    });
                }
            }
            return rtn;
        }

        public static List<HANGBAY> SelectAllArticle2()
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

        public static List<MAYBAY> SelectAllArticle3()
        {
            var rtn = new List<MAYBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.MAYBAYs)
                {
                    rtn.Add(new MAYBAY
                    {
                        IDMayBay = item.IDMayBay,
                        SoDangBa = item.SoDangBa,
                        LoaiMayBay = item.LoaiMayBay,
                        HangBay = item.HANGBAY1.TenHangbay
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
                return View(database.CHUYENBAYs.ToList());
            }
            else
            {
                return View(database.CHUYENBAYs.Where(s => s.HangBay.Trim() == database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim()));
            }
        }
        public ActionResult Create()
        {
            var username = User.Identity.Name;
            string data = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
            if ((database.CHUCVUs.Where(x => x.IDChucVu.Trim() == data).FirstOrDefault().IsAdmin) == "true")
            {
                List<MAYBAY> list2 = SelectAllArticle3().ToList();
                List<HANGBAY> list1 = SelectAllArticle2().ToList();
                List<SANBAY> list = SelectAllArticle1().ToList();
                ViewBag.listMayBay = new SelectList(list2, "IDMayBay", "IDMayBay", "");
                ViewBag.listHangBay = new SelectList(list1, "IDHangBay", "TenHangBay", "");
                ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", "");
                CHUYENBAY cb = new CHUYENBAY();
                return View(cb);
            }
            else
            {
                List<MAYBAY> list2 = SelectAllArticle3().ToList();
                List<HANGBAY> list1 = SelectAllArticle2().ToList();
                List<SANBAY> list = SelectAllArticle1().ToList();
                ViewBag.listMayBay = new SelectList(list2, "IDMayBay", "IDMayBay", "");
                ViewBag.listHangBay = new SelectList(list1, "IDHangBay", "TenHangBay", "");
                ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", "");
                CHUYENBAY cb = new CHUYENBAY();
                return View(cb);
            }    
                
        }
        [HttpPost]
        public ActionResult Create(CHUYENBAY cb)
        {
            List<MAYBAY> list2 = SelectAllArticle3().ToList();
            List<HANGBAY> list1 = SelectAllArticle2().ToList();
            List<SANBAY> list = SelectAllArticle1().ToList();
            try
            {
                ViewBag.listMayBay = new SelectList(list2, "IDMayBay", "IDMaybay", 1);
                ViewBag.listHangBay = new SelectList(list1, "IDHangBay", "TenHangBay", 1);
                ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", 1);
                if(cb.SoGheHang1 > database.MAYBAYs.Where(s => s.IDMayBay == cb.IDChuyenBay).FirstOrDefault().SucChuaToiDa || 
                    cb.SoGheHang1 > database.MAYBAYs.Where(s => s.IDMayBay == cb.IDChuyenBay).FirstOrDefault().SucChuaToiDa ||
                    (cb.SoGheHang1 + cb.SoGheHang2) > database.MAYBAYs.Where(s => s.IDMayBay == cb.IDChuyenBay).FirstOrDefault().SucChuaToiDa)
                {
                    ModelState.AddModelError("CredentialError", "Vui lòng kiểm tra lại số ghế hạng 1 và hạng 2, số ghế hạng 1 và hạng 2 không được lớn hơn sức chứa tối đa của máy bay");
                    return View("Create");
                } 
                else
                {
                    database.CHUYENBAYs.Add(cb);
                    database.SaveChanges();
                }                   
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int Id)
        {
            return View(database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            List<MAYBAY> list2 = SelectAllArticle3().ToList();
            List<HANGBAY> list1 = SelectAllArticle2().ToList();
            List<SANBAY> list = SelectAllArticle1().ToList();
            ViewBag.listMayBay = new SelectList(list2, "IDMayBay", "IDMayBay", "");
            ViewBag.listHangBay = new SelectList(list1, "IDHangBay", "TenHangBay", "");
            ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", "");
            return View(database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, CHUYENBAY cb)
        {
            List<MAYBAY> list2 = SelectAllArticle3().ToList();
            List<HANGBAY> list1 = SelectAllArticle2().ToList();
            List<SANBAY> list = SelectAllArticle1().ToList();
            ViewBag.listMayBay = new SelectList(list2, "IDMayBay", "IDMayBay", 1);
            ViewBag.listHangBay = new SelectList(list1, "IDHangBay", "TenHangBay", 1);
            ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", 1);
            database.Entry(cb).State = System.Data.Entity.EntityState.Modified;
            if (cb.SoGheHang1 > database.MAYBAYs.Where(s => s.IDMayBay == cb.IDChuyenBay).FirstOrDefault().SucChuaToiDa ||
                    cb.SoGheHang1 > database.MAYBAYs.Where(s => s.IDMayBay == cb.IDChuyenBay).FirstOrDefault().SucChuaToiDa ||
                    (cb.SoGheHang1 + cb.SoGheHang2) > database.MAYBAYs.Where(s => s.IDMayBay == cb.IDChuyenBay).FirstOrDefault().SucChuaToiDa)
            {
                ModelState.AddModelError("CredentialError", "Vui lòng kiểm tra lại số ghế hạng 1 và hạng 2, số ghế hạng 1 và hạng 2 không được lớn hơn sức chứa tối đa của máy bay");
                return View("Edit");
            }
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, CHUYENBAY cb)
        {
            try
            {
                cb = database.CHUYENBAYs.Where(s => s.ID == Id).FirstOrDefault();
                database.CHUYENBAYs.Remove(cb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Flight");
            }
        }
    }
}