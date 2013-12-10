using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models.PictureModels;
using Domain;
using System.Text.RegularExpressions;

namespace MvcApplication.Controllers
{
    public class PictureController : BaseController
    {
        [HttpGet]
        public ActionResult AddPicture(int userID, int pictureSetID)
        {
            ViewBag.UserID = userID;
            ViewBag.PictureSetID = pictureSetID;
            ViewBag.NoImage = null;
            return View();
        }

        [HttpPost]
        public ActionResult AddPicture(PictureModel obj, HttpPostedFileBase image, int userID, int pictureSetID)
        {
            ViewBag.UserID = userID;
            ViewBag.PictureSetID = pictureSetID;
            ViewBag.NoImage = null;
            if (image == null)
            {
                ViewBag.NoImage = "Sorry, but you haven't uploaded the image";
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                obj.PictureMimeType = image.ContentType;
                obj.PictureData = new byte[image.ContentLength];
                image.InputStream.Read(obj.PictureData, 0, image.ContentLength);
                core.PictureRepository.CreatePicture(obj.Name,obj.ShortInfo,obj.PictureData,obj.PictureMimeType, pictureSetID, DateTime.UtcNow);
                var picture = core.PictureRepository.Find(pictureSetID, obj.Name);

                string pattern = "(, )|(,)|( , )";

                string[] tags = Regex.Split(obj.TagsLine, pattern,RegexOptions.IgnoreCase);
                foreach (string match in tags)
                    if ((String.Compare(match, ",") != 0) && (String.Compare(match, ", ") != 0) && (String.Compare(match, " , ") != 0))
                    {
                        core.TagRepository.Create(match);
                        var tag = core.TagRepository.Find(match);
                        tag.Pictures = new List<Picture>();
                        tag.Pictures.Add(picture);
                        core.Submit();
                    }
                return RedirectToRoute("ViewPictureSet", new { userID = userID, pictureSetID = pictureSetID });
            }
            else
                return View(obj);
        }

    }
}
