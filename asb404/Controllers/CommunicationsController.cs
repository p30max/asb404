using Asb404.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace Asb404.Controllers
{
    [Authorize]
    public class CommunicationsController : Controller
    {
        private readonly DBContexter _db = new DBContexter();

        public ActionResult Messages()
        {
            var model = _db.ContactMessages.OrderByDescending(x => x.Id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult MarkRead(int id)
        {
            var msg = _db.ContactMessages.Find(id);
            if (msg != null)
            {
                msg.Status = "Read";
                _db.SaveChanges();
            }
            return RedirectToAction("Messages");
        }

        public ActionResult DeleteMessage(int id)
        {
            var msg = _db.ContactMessages.Find(id);
            if (msg != null)
            {
                _db.ContactMessages.Remove(msg);
                _db.SaveChanges();
            }
            return RedirectToAction("Messages");
        }

        public ActionResult Subscribers()
        {
            return View(_db.Subscribes.OrderByDescending(x => x.ID).ToList());
        }

        public ActionResult DeleteSubscriber(int id)
        {
            var subscriber = _db.Subscribes.Find(id);
            if (subscriber != null)
            {
                _db.Subscribes.Remove(subscriber);
                _db.SaveChanges();
            }
            return RedirectToAction("Subscribers");
        }

        [HttpGet]
        public ActionResult Newsletter()
        {
            ViewBag.SubscriberCount = _db.Subscribes.Count();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Newsletter(string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
            {
                ViewBag.msg = "موضوع و متن خبر را وارد کنید";
                ViewBag.SubscriberCount = _db.Subscribes.Count();
                return View();
            }

            var subscribers = _db.Subscribes.Select(x => x.Email).Where(x => x != null && x != "").Distinct().ToList();
            if (!subscribers.Any())
            {
                ViewBag.msg = "هیچ مشترکی برای ارسال وجود ندارد";
                ViewBag.SubscriberCount = 0;
                return View();
            }

            var validate = new Validate();
            var sent = 0;
            foreach (var email in subscribers)
            {
                try
                {
                    var message = new MailMessage();
                    message.To.Add(email);
                    message.From = new MailAddress("info@asb404.ir");
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.BodyEncoding = System.Text.Encoding.UTF8;

                    var smtp = new SmtpClient("asb404.ir", 25);
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("info@asb404.ir", "79030712");
                    smtp.Send(message);
                    sent++;
                }
                catch
                {
                    // continue sending to the rest
                }
            }

            ViewBag.msg = string.Format("ارسال شد برای {0} نفر از {1} مشترک", sent, subscribers.Count);
            ViewBag.SubscriberCount = subscribers.Count;
            return View();
        }
    }
}
