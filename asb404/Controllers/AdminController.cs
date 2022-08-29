using Asb404.Models;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asb404.Controllers
{
    public class AdminController : Controller
    {
        DBContexter _db = new DBContexter();
        PDate _dater = new PDate();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
  
        public ActionResult _Banner_List()
        {
            return PartialView(_db.Banner);
        }
        [HttpGet]
        public ActionResult Banner_Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Banner_Add(Banner model, HttpPostedFileBase file, Validate vlt)
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
                    if ((oImage.Width < 1200) || (oImage.Height < 525))
                    {

                        ViewBag.msg = "سایز عکس باید بیشتراز 1200*525 پیکسل باشد.";
                    }
                    else

                    if (file != null && file.ContentLength != 0)
                    {
                        //string UrlFolder = _db.AllNews.Find(model.Group).;
                       // string UrlFolder = _db.Banner.Find(model.Id).Id.ToString();
                        string pathForSaving = Server.MapPath("/images/Banner/" /*+ UrlFolder + "/"*/);
                       // string pathl1 = Server.MapPath("/images/Article/" + UrlFolder + "/");
                 

                      vlt.CreateFolderIfNeeded(pathForSaving);
                    


                        try
                        {
                            string OrderName;
                            if (model.Id == 0)
                            {
                                OrderName = _dater.FileNameCreator(model.Id, Path.GetFileName(file.FileName));
                            }
                            else
                                OrderName = model.Image;
                            model.Image = OrderName;
                            //  string OrderName = file.FileName;
                            file.SaveAs(Path.Combine(pathForSaving, OrderName));
                            //isUploaded = true;
                            string path1 = Server.MapPath("/images/Banner/"  + OrderName);
        

                            model.Image = ("/images/Banner/"+ OrderName);
                    


                            ResizeSettings resizeSetting1 = new ResizeSettings
                            {
                                Mode = FitMode.Stretch,
                                Width = 1600,
                                Height = 700,


                                //Format = "jpg"
                                Format = System.IO.Path.GetExtension(file.FileName).ToLower()
                            };
                            ImageBuilder.Current.Build(path1, path1, resizeSetting1);

                          

                            if (model.Id == 0) _db.Banner.Add(model);
                            else
                            {

                                _db.Entry(model).State = EntityState.Modified;
                            }

                            _db.SaveChanges();
                            ViewBag.msg = "عکس با موفقیت آپلود شد";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
                        }


                    }

                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Baner_Edit(Banner model, HttpPostedFileBase file,Validate vlt)
        {

            if (ModelState.IsValid)

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
                        if ((oImage.Width < 1920) || (oImage.Height < 900))
                        {

                            ViewBag.msg = "سایز عکس باید بیشتراز 1920*900 پیکسل باشد.";
                        }
                        else

                        if (file != null && file.ContentLength != 0)
                        {
                            //string UrlFolder = _db.AllNews.Find(model.Group).;
                            string UrlFolder = "";
                            string pathForSaving = Server.MapPath("/images/Banner/" + UrlFolder + "/");
                            string pathl1 = Server.MapPath("/images/Banner/" + UrlFolder + "/");


                            vlt.CreateFolderIfNeeded(pathForSaving);
                            vlt.CreateFolderIfNeeded(pathl1);



                            try
                            {
                                string OrderName;

                                OrderName = "home-slider-slide-x.jpg";
                                model.Image = OrderName;
                                //  string OrderName = file.FileName;
                                file.SaveAs(Path.Combine(pathForSaving, OrderName));
                                //isUploaded = true;
                                string path1 = Server.MapPath("/images/Banner/" + UrlFolder + "/" + OrderName);


                                model.Image = ("/images/Banner/" + UrlFolder + "/" + OrderName);



                                ResizeSettings resizeSetting1 = new ResizeSettings
                                {
                                    Mode = FitMode.Stretch,
                                    Width = 870,
                                    Height = 412,


                                    //Format = "jpg"
                                    Format = System.IO.Path.GetExtension(file.FileName).ToLower()
                                };
                                ImageBuilder.Current.Build(path1, path1, resizeSetting1);





                            }
                            catch (Exception ex)
                            {
                                ViewBag.msg = string.Format("File upload failed: {0}", ex.Message);
                            }
                        }
                    }
                }
                _db.Entry(model).State = EntityState.Modified;


                _db.SaveChanges();
                ViewBag.msg = "عکس با موفقیت آپلود شد";




            }


            return View();
        }

        [HttpDelete]
        public ActionResult Banner_Delete(int? Id)
        {
            return View();
        }
    }
}