using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true")]
    public class CategoriesController : Controller
    {
        // GET: Categories
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            return View(database.PHONGBANs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PHONGBAN Phongban)
        {
            try
            {
                database.PHONGBANs.Add(Phongban);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int Id)
        {
            return View(database.PHONGBANs.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            return View(database.PHONGBANs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, PHONGBAN Phongban)
        {
            database.Entry(Phongban).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(database.PHONGBANs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, PHONGBAN Phongban)
        {
            try
            {
                Phongban = database.PHONGBANs.Where(s => s.ID == Id).FirstOrDefault();
                database.PHONGBANs.Remove(Phongban);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Category");
            }
        }
    }
}