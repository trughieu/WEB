using HeThongBanVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongBanVeMayBay.Controllers
{
    public class BookTicketController : Controller
    {
        // GET: BookTickett
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        public static List<SANBAY> SelectAllArticle()
        {
            var rtn = new List<SANBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.SANBAYs)
                {
                    rtn.Add(new SANBAY
                    {
                        ID = item.ID,
                        //IDSanBay = item.IDSanBay,
                        TenSB = item.TenSB
                    });
                }
            }
            return rtn;
        }
        public List<HangVe> GetAllCategories()
        {
            var hangVes = new List<HangVe>
            {
                new HangVe {Id = 1, Name = "Hạng thương gia"},
                new HangVe {Id = 2, Name = "Hạng phổ thông"},
            };

            return hangVes.ToList();
        }

        public ActionResult Index()
        {
            ViewBag.HangVeList = new SelectList(GetAllCategories(), "Id", "Name");           
            List<SANBAY> list = SelectAllArticle().ToList();
            ViewBag.listSanBay = new SelectList(list, "IATA", "TenSB", 1);
            return View();
        } 
        public ActionResult Search(DateTime? DepartDate, DateTime? ReturnDate)
        {
            //if(ReturnDate == null)
            //{
            //    //var list = database.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingFrom + "' AND IDSanBayDen = '" + FlyingTo + "' AND NgayGio = '" + DepartDate.Value.ToString("yyyy/MM/dd") + "'").ToList();
            //    //return View(list);
            //    return RedirectToAction("OneWay", "BookTicket");
            //}
            //else
            //{
            //    //var list = database.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingFrom + "' AND IDSanBayDen = '" + FlyingTo + "' AND NgayGio = '" + DepartDate.Value.ToString("yyyy/MM/dd") + "'").ToList();
            //    //var list1 = database.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingTo + "' AND IDSanBayDen = '" + FlyingFrom + "' AND NgayGio = '" + ReturnDate.Value.ToString("yyyy/MM/dd") + "'").ToList();
            //    //return View(list);
            //}
            Session["ReturnDate"] = ReturnDate;
            return View();
        }

        public ActionResult SearchDatCho(string madatcho, MADATCHO mdc)
        {
            var rtn = from v in database.VECHUYENBAYs
                      join c in database.CHUYENBAYs on v.IDChuyenBay equals c.ID
                      where v.IDVeChuyenBay == madatcho
                      select new { IDSanBayDi = c.SANBAY.TenSB, IDSanBayDen = c.SANBAY1.TenSB, HangBay = c.HANGBAY1.TenHangbay, NgayBay = c.NgayBay, GioBay = c.GioBay, GiaTien = v.GiaTien };
            mdc.IDSanBayDi = rtn.Select(a => a.IDSanBayDi).FirstOrDefault();
            mdc.IDSanBayDen = rtn.Select(a => a.IDSanBayDen).FirstOrDefault();
            mdc.HangBay = rtn.Select(a => a.HangBay).FirstOrDefault();
            mdc.NgayBay = rtn.Select(a => a.NgayBay).FirstOrDefault();
            mdc.GioBay = rtn.Select(a => a.GioBay).FirstOrDefault();
            mdc.GiaTien = rtn.Select(a => a.GiaTien).FirstOrDefault();
            return View(mdc);
        }
        public static List<CHUYENBAY> OneWay(string FlyingFrom, string FlyingTo, DateTime? DepartDate, string SoLuong)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingFrom + "' AND IDSanBayDen = '" + FlyingTo + "' AND NgayBay = '" + DepartDate.Value.ToString("yyyy/MM/dd") + "'").ToList())
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
        public static List<CHUYENBAY> TwoWay(string FlyingFrom, string FlyingTo, DateTime? ReturnDate)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE IDSanBayDi = '" + FlyingTo + "' AND IDSanBayDen = '" + FlyingFrom + "' AND NgayBay = '" + ReturnDate.Value.ToString("yyyy/MM/dd") + "'").ToList())
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

        public PartialViewResult RenderOneWay(string FlyingFrom, string FlyingTo, DateTime? DepartDate, DateTime? ReturnDate, string Adult)
        {
            Session["Adult"] = Adult;
            return PartialView(OneWay(FlyingFrom, FlyingTo, DepartDate, Adult));
        }

        public PartialViewResult RenderTwoWay(string FlyingFrom, string FlyingTo, DateTime? DepartDate, DateTime? ReturnDate)
        {
            return PartialView(TwoWay(FlyingFrom, FlyingTo, ReturnDate));              
        }

        public List<CHUYENBAY> ChooseOneWay(int Id)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE ID = "+ Id +"").ToList())
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,
                        IDChuyenBay = item.IDChuyenBay,
                        HangBay = item.HANGBAY1.TenHangbay,
                        IDSanBayDen = item.SANBAY1.TenSB,
                        IDSanBayDi = item.SANBAY.TenSB,
                        GiaTien = item.GiaTien * Convert.ToInt32(Session["Adult"]),
                        NgayBay = item.NgayBay.Date,
                        GioBay = item.GioBay,
                        ThoiGianToiDuKien = item.ThoiGianToiDuKien,
                    });
                }
            }
            return rtn;
        }

        public List<CHUYENBAY> ChooseTwoWay(int Id)
        {
            var rtn = new List<CHUYENBAY>();
            using (var context = new QLBANVEMAYBAYEntities())
            {
                foreach (var item in context.CHUYENBAYs.SqlQuery("SELECT * FROM CHUYENBAY WHERE ID = " + Id + "").ToList())
                {
                    rtn.Add(new CHUYENBAY
                    {
                        ID = item.ID,
                        IDChuyenBay = item.IDChuyenBay,
                        HangBay = item.HANGBAY1.TenHangbay,
                        IDSanBayDen = item.SANBAY1.TenSB,
                        IDSanBayDi = item.SANBAY.TenSB,
                        GiaTien = item.GiaTien * Convert.ToInt32(Session["Adult"]),
                        NgayBay = item.NgayBay.Date,
                        GioBay = item.GioBay,
                        ThoiGianToiDuKien = item.ThoiGianToiDuKien,
                    });
                }
            }
            return rtn;
        }

        public PartialViewResult OrderOneWay(int Id)
        {
            Session["IDChieuDi"] = Id;
            return PartialView(ChooseOneWay(Id));
        }

        public PartialViewResult OrderTwoWay(int Id)
        {            
            return PartialView(ChooseTwoWay(Id));
        }

        public ActionResult Accept(int motchieu, int khuhoi)
        {
            if (khuhoi == 0)
            {
                string url = "/BookTicket/OrderOneWay/" + motchieu;              
                return Redirect(url);
            }    
            else
            {
                string url = "/BookTicket/OrderOneWay/" + motchieu;
                Session["IDChieuVe"] = khuhoi;
                return Redirect(url);
            }    
        }    
    }
}