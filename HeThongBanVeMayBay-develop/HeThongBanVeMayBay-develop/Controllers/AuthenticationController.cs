using HeThongBanVeMayBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HeThongBanVeMayBay.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        QLBANVEMAYBAYEntities database = new QLBANVEMAYBAYEntities();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(NHANVIEN nv, string returnurl)
        {
            if (ModelState.IsValid)
            {
                UserStatus status = GetUserValidity(nv);
                bool IsAdmin = false;
                //var returnurl = RedirectToAction("Index", "Home");
                string returnURL = "";
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Tài khoản hoặc mật khẩu không đúng");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(nv.UserName, false);
                Session["IsAdmin"] = IsAdmin;
                if (IsAdmin == true)
                {
                    if (returnurl == null)
                    {
                        returnurl = "/Employee/Index";
                    }
                    returnURL = returnurl;
                }
                else if (IsAdmin == false)
                {
                    foreach (var item in database.NHANVIENs.Where(x => x.UserName == nv.UserName && x.Pass == nv.Pass))
                    {
                        //returnurl = RedirectToAction("Edit", "Employee", new { id = item.ID });
                        returnURL = "/Employee/Edit/" + item.ID;
                    }
                }
                return Redirect(returnURL);
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult LoginHK()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginHK(HANHKHACH kh, string returnurl)
        {
            if(ModelState.IsValid)
            {
                UserStatus status = GetUserValidity1(kh);
                if(status == UserStatus.NonAuthenticatedUser)
                {
                    FormsAuthentication.SetAuthCookie(kh.UserName, false);
                    Session["IsCustomer"] = kh.UserName;
                    if (returnurl == null)
                    {
                        returnurl = "/BookTicket/Index";
                        return Redirect(returnurl);
                    }    
                    else
                    {
                        return Redirect(returnurl);
                    }    
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Tài khoản hoặc mật khẩu không đúng");
                    return View("LoginHK");
                }               
            }    
            else
            {
                return View("LoginHK");
            }    
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public UserStatus GetUserValidity(NHANVIEN nv)
        {
            var status = UserStatus.NonAuthenticatedUser;
            foreach (var item in database.NHANVIENs.Where(x => x.UserName == nv.UserName && x.Pass == nv.Pass))
            {
                if (database.CHUCVUs.Where(x => x.IDChucVu.Trim() == item.ChucVu.Trim()).FirstOrDefault().IsAdmin == "true" || database.CHUCVUs.Where(x => x.IDChucVu.Trim() == item.ChucVu.Trim()).FirstOrDefault().IsAdmin == "diffe")
                {
                    status = UserStatus.AuthenticatedAdmin;
                }
                else if (database.CHUCVUs.Where(x => x.IDChucVu.Trim() == item.ChucVu.Trim()).FirstOrDefault().IsAdmin == "false")
                {
                    status = UserStatus.AuthenticatedUser;
                }
                else
                    status = UserStatus.NonAuthenticatedUser;
            }
            return status;
        }
        public UserStatus GetUserValidity1(HANHKHACH kh)
        {
            var status = UserStatus.NonAuthenticatedUser;
            string check_user = null;
            var get_user_from_database = from u in database.HANHKHACHes 
                                         where u.UserName == kh.UserName && u.Pass == kh.Pass
                                         select new { u.UserName };
            check_user = get_user_from_database.Select(a => a.UserName).FirstOrDefault();
            if (check_user == null)
            {
                status = UserStatus.Unknow;
                return status;
            }
            else
            {
                return status;
            }    
        }    
    }
}