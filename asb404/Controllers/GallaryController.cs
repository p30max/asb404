using Asb404.Models;
using ImageResizer;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asb404.Controllers
{

    public class GallaryController : Controller
    {
        DBContexter _db = new DBContexter();
        Tools _tools = new Tools();
        [Authorize]
        public ActionResult Delete(int? id)
        {
            string fullPath = Request.MapPath(_db.Gallaries.Find(id).image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }
            _db.Gallaries.Remove(_db.Gallaries.Find(id));
            _db.SaveChanges();
            return RedirectToAction("ShowGallary", "Project");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Add(int? id)
        {
            TempData["Cid"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Gallary model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                {

                    if (file == null)
                    {


                        ViewBag.msg = "لطفاً عکس مورد نظر را انتخاب کنید";

                    }
                    else
                    if (System.IO.Path.GetExtension(file.FileName).ToLower() != ".jpg" && System.IO.Path.GetExtension(file.FileName).ToLower() != ".jpeg")
                    {
                        ViewBag.msg = "عکس مورد نظر فقط باید jpg باشد";
                    }
                    else
                      if (!(file.ContentLength < 1024 * 8000))
                    {
                        ViewBag.msg = "عکس نباید از 8000 کیلو بایت بیشتر باشد";
                    }
                    else
                    {
                        Image oImage = Image.FromStream(file.InputStream);
                        if ((oImage.Width > 8192) || (oImage.Height > 6144))
                        {

                            ViewBag.msg = "سایز عکس نباید از 4 k بیشتر باشد";
                        }
                        else

                        if (file != null && file.ContentLength != 0)
                        {
                            string UrlFolder = "/Images/" + "Gallary";
                            string pathForSaving = Server.MapPath(UrlFolder);
                            if (_tools.CreateFolderIfNeeded(pathForSaving))
                            {
                                try
                                {
                                    string OrderName = (int)((DateTime.Now.Ticks / 10) % 1000000) + System.IO.Path.GetExtension(file.FileName).ToLower();
                                    file.SaveAs(Path.Combine(pathForSaving, OrderName));
                                    //isUploaded = true;
                                    ViewBag.msg = "عکس با موفقیت آپلود شد";
                                    model.image = (UrlFolder + "/" + OrderName);

                                    _db.Gallaries.Add(model);
                                    _db.SaveChanges();

                                    string path1 = Server.MapPath(UrlFolder + "/" + OrderName);
                                    string path2 = Server.MapPath(UrlFolder + "/" + OrderName);


                                    ResizeSettings resizeSetting = new ResizeSettings
                                    {
                                        Mode = FitMode.Stretch,
                                        Width = 800
                                     ,

                                        Height = 600,
                                        Format = "jpg"
                                    };
                                    ImageBuilder.Current.Build(path1, path2, resizeSetting);

                                }
                                catch (Exception ex)
                                {
                                    ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
                                }
                            }
                        }

                    }




                }
            }


            return View();
        }



        public ActionResult List()
        {
            return View();
        }

        public ActionResult _List(int? id, int? page)
        {
            List<Gallary> Gl = new List<Gallary>();
            if (id != null)
            {
                Gl = _db.Gallaries.Where(x => x.idx == id).ToList();

            }
            else
            {
                Gl = _db.Gallaries.ToList();
            }
            var pagenumber = page ?? 1;
            var Apage = Gl.ToPagedList(pagenumber, 28);
            return PartialView(Apage);
        }


        // GET: Gallary
        public ActionResult Index(int? id, int? page)
        {
            List<Gallary> Gl = new List<Gallary>();
            if (id != null)
            {
                Gl = _db.Gallaries.Where(x => x.idx == id).ToList();

            }
            else
            {
                Gl = _db.Gallaries.ToList();
            }
            
            return View(Gl);
        }
    }
}