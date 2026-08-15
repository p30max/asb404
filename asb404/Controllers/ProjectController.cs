using Asb404.Models;
using ImageResizer;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asb404.Controllers
{
    public class ProjectController : Controller
    {
        private readonly DBContexter _db = new DBContexter();
        private readonly Tools _tools = new Tools();
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".png" };

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Project model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View();

            if (file == null)
            {
                ViewBag.msg = "لطفاً عکس مورد نظر را انتخاب کنید";
                return View();
            }

            string inputExtension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedImageExtensions.Contains(inputExtension))
            {
                ViewBag.msg = "فقط فایل تصویری معتبر انتخاب کنید";
                return View();
            }

            if (!(file.ContentLength < 1024 * 8000))
            {
                ViewBag.msg = "عکس نباید از 8000 کیلو بایت بیشتر باشد";
                return View();
            }

            Image oImage = Image.FromStream(file.InputStream);
            if ((oImage.Width > 8192) || (oImage.Height > 6144))
            {
                ViewBag.msg = "سایز عکس نباید از 4k بیشتر باشد";
                return View();
            }

            if (file.InputStream.CanSeek)
            {
                file.InputStream.Position = 0;
            }

            if (_db.Projects.Any(x => x.Customer == model.Customer))
            {
                ViewBag.msg = "نام پروژه شما قبلاٌ انتخاب شده لطفٌ مجدد انتخاب کنید";
                return View();
            }

            string pathForSaving = Server.MapPath("~/Images/");
            if (!_tools.CreateFolderIfNeeded(pathForSaving))
            {
                ViewBag.msg = "ساخت پوشه تصاویر با خطا مواجه شد";
                return View();
            }

            try
            {
                string baseName = Guid.NewGuid().ToString("N");
                string tempName = baseName + inputExtension;
                string outputName = baseName + ".jpg";
                string tempPath = Path.Combine(pathForSaving, tempName);
                string outputPath = Path.Combine(pathForSaving, outputName);

                file.SaveAs(tempPath);

                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Mode = FitMode.Stretch,
                    Width = 600,
                    Height = 400,
                    Format = "jpg"
                };

                ImageBuilder.Current.Build(tempPath, outputPath, resizeSetting);

                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);

                model.image = "/Images/" + outputName;
                _db.Projects.Add(model);
                _db.SaveChanges();

                ViewBag.msg = "عکس با موفقیت آپلود شد";
            }
            catch (Exception ex)
            {
                ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
            }

            return View();
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            var project = _db.Projects.Find(id);
            if (project == null)
                return RedirectToAction("list");

            string fullPath = Request.MapPath(project.image);
            if (!string.IsNullOrWhiteSpace(fullPath) && System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _db.Projects.Remove(project);
            _db.SaveChanges();
            return RedirectToAction("list");
        }

        public ActionResult Index(int id)
        {
            ViewBag.groupId = id;
            return View();
        }

        public ActionResult _List(int id)
        {
            return PartialView(_db.Projects.Where(x => x.GroupId == id).ToList());
        }

        public ActionResult Detail(int? id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult list()
        {
            var model = _db.Projects.OrderBy(c => c.Customer).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(_db.Projects.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Project model, HttpPostedFileBase file)
        {
            if (file == null)
            {
                if (_db.Projects.Where(x => x.Customer == model.Customer).Count() >= 2)
                {
                    ViewBag.msg = "نام پروژه شما قبلاٌ انتخاب شده لطفٌ مجدد انتخاب کنید";
                }
                else
                {
                    _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.msg = "ویرایش با موفقیت انجام شد";
                }

                return View();
            }

            string inputExtension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedImageExtensions.Contains(inputExtension))
            {
                ViewBag.msg = "فقط فایل تصویری معتبر انتخاب کنید";
                return View();
            }

            if (!(file.ContentLength < 1024 * 8000))
            {
                ViewBag.msg = "عکس نباید از 8000 کیلو بایت بیشتر باشد";
                return View();
            }

            Image oImage = Image.FromStream(file.InputStream);
            if ((oImage.Width > 8192) || (oImage.Height > 6144))
            {
                ViewBag.msg = "سایز عکس نباید از 4k بیشتر باشد";
                return View();
            }

            string pathForSaving = Server.MapPath("~/Images/");
            if (!_tools.CreateFolderIfNeeded(pathForSaving))
            {
                ViewBag.msg = "ساخت پوشه تصاویر با خطا مواجه شد";
                return View();
            }

            try
            {
                string baseName = Guid.NewGuid().ToString("N");
                string tempName = baseName + inputExtension;
                string outputName = baseName + ".jpg";
                string tempPath = Path.Combine(pathForSaving, tempName);
                string outputPath = Path.Combine(pathForSaving, outputName);

                file.SaveAs(tempPath);

                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Mode = FitMode.Stretch,
                    Width = 600,
                    Height = 400,
                    Format = "jpg"
                };

                ImageBuilder.Current.Build(tempPath, outputPath, resizeSetting);

                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);

                model.image = "/Images/" + outputName;
                ViewBag.msg = "عکس با موفقیت آپلود شد";
            }
            catch (Exception ex)
            {
                ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
            }

            return View();
        }
    }
}
