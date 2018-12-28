﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WEBSITESLK.Models;

namespace WEBSITESLK.Controllers
{
    public class ImagesController : Controller
    {
        private Car db = new Car();
      
           [Authorize]
        // GET: Images
        [HttpGet]
        public ActionResult AddVehicleImage(string id)
        {
            CarInfo1 imgstatus = db.CarInfo1.Find(id);
         
            return View(imgstatus);
        }
        [HttpPost]
        public ActionResult AddVehicleImage(string id,image ige, HttpPostedFileBase imgf)
        {
            if (imgf != null)
            {
                CarInfo1 cimg = new CarInfo1();
                cimg.isimgup = true;
                string filename = id+Path.GetExtension(imgf.FileName) ;
                string path = Path.Combine(Server.MapPath("~/Images/"), filename.ToString());
                imgf.SaveAs(path.ToString());
                Pics newimageupload = new Pics();
                ModelState.Clear();
                newimageupload.ImagesBlobFileInput(filename, path);

                db.SaveChanges();
               
            }
            return RedirectToAction("success");
     
        }

        public ActionResult success()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Pics(string VIN)
        {
            return View();
        }
    }
}