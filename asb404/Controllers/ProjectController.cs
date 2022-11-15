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
        DBContexter _db = new DBContexter();
        Tools _tools = new Tools();
        // GET: Project
        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Add(Project model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)

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

                        ViewBag.msg = "سایز عکس نباید از 4k بیشتر باشد";
                    }
                    else

                    if (file != null && file.ContentLength != 0)
                    {
                        // string UrlFolder = _db.GroupMain.Find(model.GroupID).Name;
                        string pathForSaving = Server.MapPath("~/Images/");
                        if (_tools.CreateFolderIfNeeded(pathForSaving))
                        {
                            try
                            {
                                string OrderName = model.Customer + System.IO.Path.GetExtension(file.FileName).ToLower();
                                file.SaveAs(Path.Combine(pathForSaving, OrderName));
                                //isUploaded = true;
                                ViewBag.msg = "عکس با موفقیت آپلود شد";
                                model.image = ("/Images/" /*+ UrlFolder + "/" */+ OrderName);
                                if (_db.Project.Where(x => x.Customer == model.Customer).Count() >= 1)
                                {
                                    ViewBag.msg = "نام پروژه شما قبلاٌ انتخاب شده لطفٌ مجدد انتخاب کنید";
                                }
                                else
                                {
                                    _db.Project.Add(model);

                                    _db.SaveChanges();
                                }
                                string path1 = Server.MapPath("/Images/" /*+ UrlFolder + "/" */+ OrderName);
                                string path2 = Server.MapPath("/Images/" /*+ UrlFolder + "/" */+ OrderName);
                                ResizeSettings resizeSetting = new ResizeSettings
                                {
                                    Mode = FitMode.Stretch,
                                    Width = 600,
                                    Height = 400,

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
            return View();
        }
        [Authorize]
        public ActionResult Delete(int? id)
        {
            string fullPath = Request.MapPath(_db.Project.Find(id).image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }
            _db.Project.Remove(_db.Project.Find(id));
            _db.SaveChanges();
            return RedirectToAction("ListProject");
        }
        public ActionResult Index(int id)
        {
            return View();
        }
        public ActionResult _List(int id)
        {
            return PartialView(_db.Project.Where(x=>x.GroupId==id).ToList());
        }
        public ActionResult Detail(int? id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        public ActionResult list()
        {
            var Model = _db.Project.OrderBy(c => c.Customer).ToList();
            //var pageNumber = page ?? 1;
            //var Apage = st.ToPagedList(pageNumber, 5);
            //ViewBag.Apage = st.ToPagedList(pageNumber, 5);
            //ViewBag.Apage = Apage;
            //return PartialView(ViewBag.Apage);
            return View(Model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(_db.Project.Find(id));
        }
        [HttpPost]

        public ActionResult Edit(Project model, HttpPostedFileBase file)
        {
            if (file != null)
            {






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

                        ViewBag.msg = "سایز عکس نباید از 4k بیشتر باشد";
                    }
                    else

                    if (file != null && file.ContentLength != 0)
                    {
                        // string UrlFolder = _db.GroupMain.Find(model.GroupID).Name;
                        string pathForSaving = Server.MapPath("~/Images/");
                        if (_tools.CreateFolderIfNeeded(pathForSaving))
                        {
                            try
                            {
                                string OrderName = model.Customer + System.IO.Path.GetExtension(file.FileName).ToLower();
                                file.SaveAs(Path.Combine(pathForSaving, OrderName));
                                //isUploaded = true;
                                ViewBag.msg = "عکس با موفقیت آپلود شد";
                                model.image = ("/Images/" /*+ UrlFolder + "/" */+ OrderName);


                                string path1 = Server.MapPath("/Images/" /*+ UrlFolder + "/" */+ OrderName);
                                string path2 = Server.MapPath("/Images/" /*+ UrlFolder + "/" */+ OrderName);
                                ResizeSettings resizeSetting = new ResizeSettings
                                {
                                    Mode = FitMode.Stretch,
                                    Width = 600,
                                    Height = 400,

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
            else
            {
                if (_db.Project.Where(x => x.Customer == model.Customer).Count() >= 2)
                {
                    ViewBag.msg = "نام پروژه شما قبلاٌ انتخاب شده لطفٌ مجدد انتخاب کنید";
                }
                else
                {
                    _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    ViewBag.msg = "ویرایش با موفقیت انجام شد";
                }

            }

            return View();
        }




    }
}