using musicapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace musicapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SongsList()
        {
            return View(GoogleDriveFilesRepository.GetDriveFiles());
        }
        [HttpPost]
        public void UploadFile(HttpPostedFileBase file)
        {
            String id = GoogleDriveFilesRepository.FileUploadInFolder("1ErWnUiDbV_hmEVcYYojELOje6LsNnBtP", file);
            if (id != null)
            {
                Response.Redirect("http://127.0.0.1:8080/songs/Create?id=" + id);
            }
        }
    }
}