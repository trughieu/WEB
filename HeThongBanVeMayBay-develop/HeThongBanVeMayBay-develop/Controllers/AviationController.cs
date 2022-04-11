using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true")]
    public class AviationController : Controller
    {      
        // GET: Aviation
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();      
        public ActionResult Index()
        {
            return View(database.HANGBAYs.ToList());
        }
        public ActionResult Create()
        {
            HANGBAY hb = new HANGBAY();
            return View(hb);
        }
        [HttpPost]
        public ActionResult Create(HANGBAY hb)
        {
            try
            {
                if (hb.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(hb.UploadImage.FileName);
                    string extent = Path.GetExtension(hb.UploadImage.FileName);
                    filename = filename + extent;
                    hb.ImageAviation = "~/Content/images/" + filename;
                    hb.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.HANGBAYs.Add(hb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int Id)
        {
            return View(database.HANGBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, HANGBAY hb)
        {
            database.Entry(hb).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (hb.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(hb.UploadImage.FileName);
                    string extent = Path.GetExtension(hb.UploadImage.FileName);
                    filename = filename + extent;
                    hb.ImageAviation = "~/Content/images/" + filename;
                    hb.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.SaveChanges();
                return RedirectToAction("Edit", Id);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int Id)
        {
            return View(database.HANGBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, HANGBAY hb)
        {
            try
            {
                hb = database.HANGBAYs.Where(s => s.ID == Id).FirstOrDefault();
                database.HANGBAYs.Remove(hb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Aviation");
            }
        }

        public ActionResult Details(int Id)
        {
            return View(database.HANGBAYs.Where(s => s.ID == Id).FirstOrDefault());
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