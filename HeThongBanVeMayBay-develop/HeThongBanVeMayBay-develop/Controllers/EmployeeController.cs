using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;
using HeThongBanVeMayBay.Ultilities;
using PagedList;
using PagedList.Mvc;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true, false")]
    public class EmployeeController : Controller
    {
        // GET: Employee
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<NHANVIEN> SelectAllArticle()
        {
            var rtn = new List<NHANVIEN>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.NHANVIENs)
                {
                    rtn.Add(new NHANVIEN
                    {
                        ID = item.ID,
                        TenNV = item.TenNV,
                        Email = item.Email,
                        GioiTinh = item.GioiTinh,
                        NgaySinh = item.NgaySinh.Date,
                        NgayVaoLam = item.NgayVaoLam.Date,
                        NgayNghiLam = item.NgayNghiLam,
                        ChucVu = item.CHUCVU1.TenChucVu,
                        BoPhan = item.PHONGBAN.TenPhongBan,
                        ImageEmp = item.ImageEmp
                    });
                }
            }
            return rtn;
        }
        public static List<PHONGBAN> SelectAllArticle1()
        {
            var rtn = new List<PHONGBAN>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.PHONGBANs)
                {
                    rtn.Add(new PHONGBAN
                    {
                        ID = item.ID,
                        IDPhongBan = item.IDPhongBan,
                        TenPhongBan = item.TenPhongBan
                    });
                }
            }
            return rtn;
        }
        public static List<CHUCVU> SelectAllArticle3()
        {
            var rtn = new List<CHUCVU>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUCVUs)
                {
                    rtn.Add(new CHUCVU
                    {
                        ID = item.ID,
                        IDChucVu = item.IDChucVu,
                        TenChucVu = item.TenChucVu,
                        IsAdmin = item.IsAdmin
                    });
                }
            }
            return rtn;
        }

        [Authorize(Roles = "true")]
        //[SessionTimeout]
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNum = (page ?? 1);
            var lsNV = SelectAllArticle().OrderBy(x => x.NgayVaoLam).ToList();
            return View(lsNV.ToPagedList(pageNum, pageSize));
            
        }

        [Authorize(Roles = "true")]
        public ActionResult Create()
        {
            List<PHONGBAN> list = SelectAllArticle1().ToList();
            List<CHUCVU> list2 = SelectAllArticle3().ToList();
            ViewBag.listChucVu = new SelectList(list2, "IDChucVu", "TenChucVu", "");
            ViewBag.listPhongBan = new SelectList(list, "IDPhongBan", "TenPhongBan", "");        
            NHANVIEN nv = new NHANVIEN();
            return View(nv);
        }
        [HttpPost]
        public ActionResult Create(NHANVIEN nv)
        {
            List<PHONGBAN> list = database.PHONGBANs.ToList();
            List<CHUCVU> list2 = SelectAllArticle3().ToList();
            try
            {
                if (nv.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(nv.UploadImage.FileName);
                    string extent = Path.GetExtension(nv.UploadImage.FileName);
                    filename = filename + extent;
                    nv.ImageEmp = "~/Content/images/" + filename;
                    nv.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listChucVu = new SelectList(list2, "IDChucVu", "TenChucVu", 1);
                ViewBag.listPhongBan = new SelectList(list, "IDPhongBan", "TenPhongBan", 1);         
                database.NHANVIENs.Add(nv);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [Authorize(Roles = "true, false")]
        public ActionResult Edit(int Id)
        {
            var username = User.Identity.Name;
            string data = database.NHANVIENs.Where(x => x.UserName == username).FirstOrDefault().ChucVu.Trim();
            var view = View();
            if ((database.CHUCVUs.Where(x => x.IDChucVu.Trim() == data).FirstOrDefault().IsAdmin) == "true")
            {
                List<PHONGBAN> list = SelectAllArticle1().ToList();
                List<CHUCVU> list2 = SelectAllArticle3().ToList();
                ViewBag.listChucVu = new SelectList(list2, "IDChucVu", "TenChucVu", "");
                ViewBag.listPhongBan = new SelectList(list, "IDPhongBan", "TenPhongBan", "");
                view = View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
            }    
            else if ((database.CHUCVUs.Where(x => x.IDChucVu.Trim() == data).FirstOrDefault().IsAdmin) == "false")
            {
                Id = database.NHANVIENs.Where(s => s.UserName == username).FirstOrDefault().ID;
                List<PHONGBAN> list = SelectAllArticle1().ToList();
                List<CHUCVU> list2 = SelectAllArticle3().ToList();
                ViewBag.listChucVu = new SelectList(list2, "IDChucVu", "TenChucVu", "");
                ViewBag.listPhongBan = new SelectList(list, "IDPhongBan", "TenPhongBan", "");
                view = View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
            }                      
            return view;
        }
        [HttpPost]
        public ActionResult Edit(int Id, NHANVIEN nv)
        {
            List<PHONGBAN> list = SelectAllArticle1().ToList();
            List<CHUCVU> list2 = SelectAllArticle3().ToList();
            var username = User.Identity.Name;            
            database.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            if ((database.NHANVIENs.Where(s => s.UserName == username).FirstOrDefault().ChucVu.Trim()) == "Nhân viên")
            {
                nv.ChucVu = database.CHUCVUs.Where(s => s.TenChucVu == nv.ChucVu).FirstOrDefault().IDChucVu;
                nv.BoPhan = database.PHONGBANs.Where(s => s.TenPhongBan == nv.BoPhan).FirstOrDefault().IDPhongBan;
            }
            try
            {
                if (nv.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(nv.UploadImage.FileName);
                    string extent = Path.GetExtension(nv.UploadImage.FileName);
                    filename = filename + extent;
                    nv.ImageEmp = "~/Content/images/" + filename;
                    nv.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listChucVu = new SelectList(list2, "IDChucVu", "TenChucVu", 1);
                ViewBag.listPhongBan = new SelectList(list, "IDPhongBan", "TenPhongBan", 1);            
                database.SaveChanges();
                return RedirectToAction("Edit", Id);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "true")]
        public ActionResult Delete (int Id)
        {
            return View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
        }
        
        [HttpPost]
        public ActionResult Delete (int Id, NHANVIEN nv)
        {
            try
            {
                nv = database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault();
                database.NHANVIENs.Remove(nv);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Employee");
            }
        }

        [Authorize(Roles = "true, false")] 
        public ActionResult Details (int Id)
        {
            return View(database.NHANVIENs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}