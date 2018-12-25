using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace WEBSITESLK.Controllers
{
    public class ImagesController : Controller
    {//[Authorize]
        // GET: Images

            [HttpGet]
        public ActionResult AddVehicleImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVehicleImage(image ige, HttpPostedFileBase imgf)
        {
            if (imgf != null)
            {
   
             

                string filename = Path.GetFileName(imgf.FileName) ;
                string path = Path.Combine(Server.MapPath("~/Images/"), filename.ToString());
                imgf.SaveAs(path.ToString());
                Pics newimageupload = new Pics();

                ModelState.Clear();
                newimageupload.ImagesBlobFileInput(filename, path);
 
           
            }
            return View();
        }
    }
}