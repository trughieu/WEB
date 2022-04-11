using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongBanVeMayBay.Models;

namespace HeThongBanVeMayBay.Controllers
{
    [Authorize(Roles = "true, false")]
    public class AirportController : Controller
    {
        // GET: Airport
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public ActionResult Index()
        {
            return View(database.SANBAYs.ToList());
        }
        public ActionResult Create()
        {
            SANBAY sb = new SANBAY();
            return View(sb);
        }
        [HttpPost]
        public ActionResult Create(SANBAY sb)
        {
            try
            {
                database.SANBAYs.Add(sb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }

        public ActionResult Edit(int Id)
        {
            return View(database.SANBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, SANBAY sb)
        {
            database.Entry(sb).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            return View(database.SANBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }
        [Authorize(Roles = "true")]
        public ActionResult Delete(int Id)
        {
            return View(database.SANBAYs.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, SANBAY sb)
        {
            try
            {
                sb = database.SANBAYs.Where(s => s.ID == Id).FirstOrDefault();
                database.SANBAYs.Remove(sb);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Airport");
            }
        }
    }
}