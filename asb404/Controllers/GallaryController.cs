using Asb404.Models;
using ImageResizer;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asb404.Controllers
{
    public class GallaryController : Controller
    {
        private readonly DBContexter _db = new DBContexter();
        private readonly Tools _tools = new Tools();

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var gallary = _db.Gallaries.Find(id);
            if (gallary == null)
                return RedirectToAction("Index");

            if (!string.IsNullOrWhiteSpace(gallary.image))
            {
                string fullPath = Request.MapPath(gallary.image);
                if (!string.IsNullOrWhiteSpace(fullPath) && System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }

            _db.Gallaries.Remove(gallary);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add(int? id)
        {
            TempData["Cid"] = id;
            ViewBag.SelectedProjectId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Gallary model, HttpPostedFileBase file)
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
                    ViewBag.msg = "سایز عکس نباید از 4 k بیشتر باشد";
                    return View();
                }
            }

            if (file.InputStream.CanSeek)
            {
                file.InputStream.Position = 0;
            }

            string urlFolder = "~/images/Gallary";
            string pathForSaving = Server.MapPath(urlFolder);
            if (!_tools.CreateFolderIfNeeded(pathForSaving))
            {
                ViewBag.msg = "ساخت پوشه تصاویر با خطا مواجه شد";
                return View();
            }

            try
            {
                string baseName = Guid.NewGuid().ToString("N");
                string orderName = baseName + ".png";
                string sourceExt = string.IsNullOrWhiteSpace(Path.GetExtension(file.FileName)) ? ".img" : Path.GetExtension(file.FileName);
                string tempPath = Path.Combine(Path.GetTempPath(), baseName + sourceExt);
                string outputPath = Path.Combine(pathForSaving, orderName);

                file.SaveAs(tempPath);
                if (!System.IO.File.Exists(tempPath))
                {
                    ViewBag.msg = "فایل روی سرور ذخیره نشد";
                    return View();
                }

                try
                {
                    ResizeSettings resizeSetting = new ResizeSettings
                    {
                        Mode = FitMode.Stretch,
                        Width = 800,
                        Height = 600,
                        Format = "png"
                    };

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
                    ViewBag.msg = "فایل خروجی ساخته نشد";
                    return View();
                }

                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);

                model.image = urlFolder + "/" + orderName;
                _db.Gallaries.Add(model);
                _db.SaveChanges();
                TempData["msg"] = "عکس با موفقیت آپلود شد";
                return RedirectToAction("Add", new { id = model.idx });
            }
            catch (Exception ex)
            {
                TempData["msg"] = string.Format("File upload failed: {0}", ex.ToString());
                return RedirectToAction("Add", new { id = model.idx });
            }
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult _List(int? id, int? page)
        {
            List<Gallary> gl = id != null
                ? _db.Gallaries.Where(x => x.idx == id).ToList()
                : _db.Gallaries.ToList();

            var pagenumber = page ?? 1;
            var apage = gl.ToPagedList(pagenumber, 28);
            return PartialView(apage);
        }

        public ActionResult Index(int? id, int? page)
        {
            ViewBag.SelectedProjectId = id;
            List<Gallary> gl = id != null
                ? _db.Gallaries.Where(x => x.idx == id).ToList()
                : _db.Gallaries.ToList();

            return View(gl);
        }
    }
}
