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
                return Json("لطفاً رمز عبور فعلی خود را وارد کنید", JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(NewPass))
                return Json("لطفاً رمز عبور جدید خود را وارد کنید", JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(RepPass))
                return Json("لطفاً رمز عبور جدید خود را تکرار کنید", JsonRequestBehavior.AllowGet);

            string hashedOld = Tools.HashPassword(OldPass);
            string username   = User.Identity.Name;

            if (!_db.Users.Any(x => x.Password == hashedOld && x.UserName == username))
                return Json("رمز عبور فعلی شما درست نمی باشد لطفاً دوباره وارد کنید", JsonRequestBehavior.AllowGet);

            if (NewPass != RepPass)
                return Json("رمز عبور شما با تکرار آن برابر نیست لطفاً مجدد وارد کنید", JsonRequestBehavior.AllowGet);

            var user = _db.Users.First(x => x.Password == hashedOld && x.UserName == username);
            user.Password = Tools.HashPassword(NewPass);
            _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return Json("رمز عبور شما تغییر یافت", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string password, bool remember, string ReturnUrl)
        {
            if (string.IsNullOrEmpty(Username))
                return Json("نام کاربری خود را وارد کنید", JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(password))
                return Json("رمز عبور خود را وارد کنید", JsonRequestBehavior.AllowGet);

            string hashedPass = Tools.HashPassword(password);

            if (!_db.Users.Any(x => x.UserName == Username && x.Password == hashedPass))
                return Json("نام کاربری یا رمز عبور شما صحیح نمی باشد", JsonRequestBehavior.AllowGet);

            string roles = _db.Users.Where(x => x.UserName == Username)
                                    .Select(x => x.Role)
                                    .FirstOrDefault();

            var authTicket = new FormsAuthenticationTicket(
                1, Username, DateTime.Now, DateTime.Now.AddMonths(1), remember, roles, "/");

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                        FormsAuthentication.Encrypt(authTicket));

            if (authTicket.IsPersistent)
                cookie.Expires = authTicket.Expiration;

            Response.Cookies.Add(cookie);

            if (string.IsNullOrEmpty(ReturnUrl))
                return JavaScript("location.href='/home/index'");

            return JavaScript(string.Format("location.href='{0}'", ReturnUrl));
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
