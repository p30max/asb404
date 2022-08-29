using Asb404.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DBContexter _db = new DBContexter();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Ghavanin()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactMailsend(string email, string subject, string memo, Validate vlt)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json("لطفاً ایمیل خود را وارد کنید", JsonRequestBehavior.AllowGet);

            }

            if (string.IsNullOrEmpty(subject))
            {
                return Json("موضوع خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(memo))
            {
                return Json("متن مورد نظر خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }
            
               if (vlt.ContactMail(email, subject, memo))
            {
                return Json("درخواست شما ارسال شد", JsonRequestBehavior.AllowGet);
            }
            
else
                return Json("درخواست شما ارسال نشد", JsonRequestBehavior.AllowGet);


        }
       
        public ActionResult setlocation(string lat, string lng)
        {
            return Json(lat, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddSubscriber(string email, Validate Vlt)
        {

            if (string.IsNullOrEmpty(email) == true)
            {
                return Json("لطفاً ایمیل خود را وارد کنید", JsonRequestBehavior.AllowGet);
            }
            else
        if (Vlt.IsValidEmailId(email) == false)
            {
                return Json("فرمت ایمیل شما صحیح نمی باشد لطفاٌ اصلاح کنید", JsonRequestBehavior.AllowGet);
            }
            else

                       if (_db.Subscribes.Where(x => x.Email == email).Count() > 0)
            {
                return Json("ایمیل شما قبلاً ثبت شده است", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Subscribe model = new Subscribe();
                model.Email = email;
                _db.Subscribes.Add(model);
                _db.SaveChanges();
                return Json("ایمیل شما به موفقیت ثبت شد", JsonRequestBehavior.AllowGet);

            }
        }
    }
}