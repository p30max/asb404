using Asb404.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class MemberController : Controller
    {
        DBContexter _db = new DBContexter();
        public ActionResult Slider_Add()
        {
            return View();
        }
        // GET: Member
        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string OldPass, string NewPass, string RepPass, Users Crus)
        {
            if (string.IsNullOrEmpty(OldPass))
            {
                return Json("لطفاً رمز عبور فعلی خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }
            else
                   if (string.IsNullOrEmpty(NewPass))
            {
                return Json("لطفاً رمز عبور جدید خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }
            else
                   if (string.IsNullOrEmpty(RepPass))
            {
                return Json("لطفاً رمز عبور جدید خود را تکرار کنید", JsonRequestBehavior.AllowGet);
            }
            else
          if (_db.Users.Where(x => x.Password == OldPass && x.UserName == User.Identity.Name.ToString()).Count() == 0)
            {
                return Json("رمز عبور فعلی شما درست نمی باشد لطفاً دوباره وارد کنید", JsonRequestBehavior.AllowGet);
            }
            else
          if (NewPass.CompareTo(RepPass) != 0)
            {
                return Json("رمز عبور شما با تکرار آن برابر نیست لطفاً مجدد وارد کنید", JsonRequestBehavior.AllowGet);

            }
            else
            {
                string username = User.Identity.Name.ToString();
                int id = _db.Users.Where(x => x.Password == OldPass & x.UserName == username).Select(x => x.Id).FirstOrDefault();
                var model = _db.Users.Find(id);
                model.Password = NewPass;
                _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();


                return Json("رمز عبور شما تغییر یافت", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string password, bool remember, string ReturnUrl)
        {
            if (string.IsNullOrEmpty(Username))
            {
                return Json("نام کاربری خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }
            else
               if (string.IsNullOrEmpty(password))
            {
                return Json("رمز عبور خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (_db.Users.Where(x => x.UserName == Username && x.Password == password).Count() >= 1)
                {
                    //FormsAuthentication.RedirectFromLoginPage(Username,"Admin", true);
                    string roles = _db.Users.Where(x => x.UserName == Username).Select(x => x.Role).FirstOrDefault();
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                      1,
                      Username,  //user id
                      DateTime.Now,
                      DateTime.Now.AddMonths(1),  // expiry
                      remember,  //do not remember
                      roles,
                      "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                       FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                    //string role = ("Admin");
                    //FormsAuthenticationTicket AuthTicket = new FormsAuthenticationTicket(1, Username, DateTime.Now, DateTime.Now.AddDays(+30), remember, role, FormsAuthentication.FormsCookiePath);
                    //string encryptedTicket = FormsAuthentication.Encrypt(AuthTicket);
                    //HttpCookie AuthCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    //Response.Cookies.Add(AuthCookie);


                    if (authTicket.IsPersistent)

                        cookie.Expires = authTicket.Expiration;
                    //FormsAuthentication.SetAuthCookie(Username, remember);
                    //if (string.IsNullOrEmpty(ReturnUrl))
                    //{
                    //    return JavaScript("location.href='/home/index'");
                    //}
                    //if (Request.IsAjaxRequest())
                    //{
                    //    return Json(new { ReturnUrl = ReturnUrl });
                    //}
                    //return Redirect(ReturnUrl);
                    if (string.IsNullOrEmpty(ReturnUrl))

                        return JavaScript("location.href='/home/index'");

                    else
                        return JavaScript(string.Format("location.href='{0}'", ReturnUrl));


                    //}
                    //return JavaScript("location.href=" + returnUrl + "");

                    //{
                    //    //return RedirectToAction("Index", "Home");      

                    //}


                }
                else
                {
                    return Json("نام کاربری یا رمز عبور شما صحیح نمی باشد", JsonRequestBehavior.AllowGet);
                }


            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}