using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asb404.Models;
using System.Drawing;
using System.IO;
using ImageResizer;
using System.Web.Helpers;

namespace Asb404.Controllers
{
    public class ProjectController : Controller
    {
        DBContexter _db = new DBContexter();
        // GET: Project
        [HttpGet]
        [Authorize]
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
    
        public ActionResult AddProject(Project model, HttpPostedFileBase file)
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
                        if (this.CreateFolderIfNeeded(pathForSaving))
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
        public ActionResult DeleteProject(int?id)
        {
            string fullPath =Request.MapPath( _db.Project.Find(id).image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }
            _db.Project.Remove(_db.Project.Find(id));
            _db.SaveChanges();
            return RedirectToAction("ListProject");
        }
        public ActionResult Showproject()
        {
            return View();
        }
        public ActionResult _Showproject()
        {
            return PartialView(_db.Project);
        }
        public ActionResult DetailProject(int?id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [Authorize]
        public ActionResult DeleteGallary(int?id)
        {
        string fullPath = Request.MapPath(_db.Gallaries.Find(id).image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                //Session["DeleteSuccess"] = "Yes";
            }
    _db.Gallaries.Remove(_db.Gallaries.Find(id));
            _db.SaveChanges();
            return RedirectToAction("ShowGallary","Project");
        }
        public ActionResult listProject()
        {
            var Model = _db.Project.OrderByDescending(x => x.Id).ToList().OrderBy(c=>c.Customer);
            //var pageNumber = page ?? 1;
            //var Apage = st.ToPagedList(pageNumber, 5);
            //ViewBag.Apage = st.ToPagedList(pageNumber, 5);
            //ViewBag.Apage = Apage;
            //return PartialView(ViewBag.Apage);
            return View(Model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult EditProject(int ?id)
        {
            return View(_db.Project.Find(id));
        }
        [HttpPost]
       
        public ActionResult EditProject(Project model, HttpPostedFileBase file)
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
                        if (this.CreateFolderIfNeeded(pathForSaving))
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
                                    Mode = FitMode.Stretch, Width = 600,
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
 

        [HttpGet]
        [Authorize]
        public ActionResult Addgallary(int?id)
        {
            TempData["Cid"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult Addgallary(Gallary model, HttpPostedFileBase file)
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
                             string UrlFolder= "/Images/" + "Gallary";
                            string pathForSaving = Server.MapPath(UrlFolder);
                            if (this.CreateFolderIfNeeded(pathForSaving))
                            {
                                try
                                {
                                    string OrderName = (int)((DateTime.Now.Ticks / 10) % 1000000)+ System.IO.Path.GetExtension(file.FileName).ToLower();
                                    file.SaveAs(Path.Combine(pathForSaving, OrderName));
                                    //isUploaded = true;
                                    ViewBag.msg = "عکس با موفقیت آپلود شد";
                                    model.image = ( UrlFolder + "/"+ OrderName);

                                    _db.Gallaries.Add(model);
                                    _db.SaveChanges();

                                    string path1 = Server.MapPath(UrlFolder + "/" + OrderName);
                                    string path2 = Server.MapPath(UrlFolder + "/" + OrderName);


                                    ResizeSettings resizeSetting = new ResizeSettings
                                    {
                              Mode=FitMode.Stretch,
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

        public ActionResult _ShowGallary(int?id)
        {
            if(id!=null)
            { 
            return PartialView(_db.Gallaries.Where(x=>x.idx==id));
            }
            else
            {
                return PartialView(_db.Gallaries);
            }
        }

        public ActionResult ShowGallary()
        {
            return View();
        }



        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
    }
}