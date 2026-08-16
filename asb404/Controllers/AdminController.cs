using Asb404.Models;
using ImageResizer;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asb404.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBContexter _db = new DBContexter();

        public ActionResult Index()
        {
            return RedirectToAction("Add");
        }

        public ActionResult _List()
        {
            return PartialView(_db.Banners.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Banner model, HttpPostedFileBase file, Validate vlt)
        {
            if (!ModelState.IsValid)
                return View();

            if (file == null)
            {
                ViewBag.msg = "لطفاً عکس مورد نظر را انتخاب کنید";
                return View();
            }

            if (!(file.ContentLength < 1024 * 8000))
            {
                ViewBag.msg = "عکس نباید از 8000 کیلو بایت بیشتر باشد";
                return View();
            }

            Image oImage;
            try
            {
                oImage = Image.FromStream(file.InputStream, true, true);
            }
            catch
            {
                ViewBag.msg = "فقط فایل تصویری معتبر انتخاب کنید";
                return View();
            }

            using (oImage)
            {
                if ((oImage.Width > 8192) || (oImage.Height > 6144))
                {
                    ViewBag.msg = "سایز عکس نباید از 4k بیشتر باشد";
                    return View();
                }

                if ((oImage.Width < 1200) || (oImage.Height < 525))
                {
                    ViewBag.msg = "سایز عکس باید بیشتراز 1200*525 پیکسل باشد.";
                    return View();
                }
            }

            if (file.InputStream.CanSeek)
            {
                file.InputStream.Position = 0;
            }

            string pathForSaving = Server.MapPath("/images/Banner/");
            vlt.CreateFolderIfNeeded(pathForSaving);

            try
            {
                string baseName = Guid.NewGuid().ToString("N");
                string outputName = baseName + ".png";
                string sourceExt = string.IsNullOrWhiteSpace(Path.GetExtension(file.FileName)) ? ".img" : Path.GetExtension(file.FileName);
                string tempPath = Path.Combine(pathForSaving, baseName + sourceExt);
                string outputPath = Path.Combine(pathForSaving, outputName);

                file.SaveAs(tempPath);

                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Mode = FitMode.Stretch,
                    Width = 1600,
                    Height = 700,
                    Format = "png"
                };

                try
                {
                    ImageBuilder.Current.Build(tempPath, outputPath, resizeSetting);
                }
                catch
                {
                    using (var sourceImage = Image.FromFile(tempPath))
                    using (var bitmap = new Bitmap(sourceImage))
                    {
                        bitmap.Save(outputPath, ImageFormat.Png);
                    }
                }

                if (!System.IO.File.Exists(outputPath))
                {
                    ViewBag.msg = "تبدیل تصویر انجام نشد";
                    return View();
                }

                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);

                model.Image = "/images/Banner/" + outputName;
                if (model.Id == 0)
                    _db.Banners.Add(model);
                else
                    _db.Entry(model).State = EntityState.Modified;

                _db.SaveChanges();
                ViewBag.msg = "عکس با موفقیت آپلود شد";
            }
            catch (Exception ex)
            {
                ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Banner model, HttpPostedFileBase file, Validate vlt)
        {
            if (!ModelState.IsValid)
                return View();

            if (file == null)
            {
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                ViewBag.msg = "عکس با موفقیت آپلود شد";
                return View();
            }

            if (!(file.ContentLength < 1024 * 8000))
            {
                ViewBag.msg = "عکس نباید از 8000 کیلو بایت بیشتر باشد";
                return View();
            }

            Image oImage;
            try
            {
                oImage = Image.FromStream(file.InputStream, true, true);
            }
            catch
            {
                ViewBag.msg = "فقط فایل تصویری معتبر انتخاب کنید";
                return View();
            }

            using (oImage)
            {
                if ((oImage.Width > 8192) || (oImage.Height > 6144))
                {
                    ViewBag.msg = "سایز عکس نباید از 4k بیشتر باشد";
                    return View();
                }

                if ((oImage.Width < 1920) || (oImage.Height < 900))
                {
                    ViewBag.msg = "سایز عکس باید بیشتراز 1920*900 پیکسل باشد.";
                    return View();
                }
            }

            if (file.InputStream.CanSeek)
            {
                file.InputStream.Position = 0;
            }

            string pathForSaving = Server.MapPath("/images/Banner/");
            vlt.CreateFolderIfNeeded(pathForSaving);

            try
            {
                string baseName = Guid.NewGuid().ToString("N");
                string outputName = baseName + ".png";
                string sourceExt = string.IsNullOrWhiteSpace(Path.GetExtension(file.FileName)) ? ".img" : Path.GetExtension(file.FileName);
                string tempPath = Path.Combine(pathForSaving, baseName + sourceExt);
                string outputPath = Path.Combine(pathForSaving, outputName);

                file.SaveAs(tempPath);

                ResizeSettings resizeSetting = new ResizeSettings
                {
                    Mode = FitMode.Stretch,
                    Width = 870,
                    Height = 412,
                    Format = "png"
                };

                try
                {
                    ImageBuilder.Current.Build(tempPath, outputPath, resizeSetting);
                }
                catch
                {
                    using (var sourceImage = Image.FromFile(tempPath))
                    using (var bitmap = new Bitmap(sourceImage))
                    {
                        bitmap.Save(outputPath, ImageFormat.Png);
                    }
                }

                if (!System.IO.File.Exists(outputPath))
                {
                    ViewBag.msg = "تبدیل تصویر انجام نشد";
                    return View();
                }

                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);

                model.Image = "/images/Banner/" + outputName;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                ViewBag.msg = "عکس با موفقیت آپلود شد";
            }
            catch (Exception ex)
            {
                ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            var banner = _db.Banners.Find(Id);
            if (banner == null)
                return RedirectToAction("Add");

            string fullMaph = Request.MapPath(banner.Image);
            if (System.IO.File.Exists(fullMaph))
                System.IO.File.Delete(fullMaph);

            _db.Banners.Remove(banner);
            _db.SaveChanges();
            return RedirectToAction("Add");
        }
    }
}
