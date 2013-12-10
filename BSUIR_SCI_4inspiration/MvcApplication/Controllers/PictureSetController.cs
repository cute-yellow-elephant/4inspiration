using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models.PictureSetModels;

namespace MvcApplication.Controllers
{
    public class PictureSetController : BaseController
    {
        [HttpGet]
        public ActionResult AddPictureSet(int userID)
        {
            ViewBag.UserID = userID;
            return View();
        }

        [HttpPost]
        public ActionResult AddPictureSet(PictureSetModel obj, HttpPostedFileBase image, int userID)
        {
            ViewBag.UserID = userID;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    obj.CoverMimeType = image.ContentType;
                    obj.CoverData = new byte[image.ContentLength];
                    image.InputStream.Read(obj.CoverData, 0, image.ContentLength);
                }
                core.PictureSetRepository.Create(obj.Name, DateTime.UtcNow, obj.CoverData, obj.CoverMimeType, core.ProfileRepository.Find(userID).ID);
                return RedirectToRoute("ProfilePageSets", new { userID = userID });
            }
            else
                return View(obj);
        }

    }
}
