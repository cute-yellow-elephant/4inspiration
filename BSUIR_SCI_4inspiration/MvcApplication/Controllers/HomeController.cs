using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models.HomeModels;
using MvcApplication.Infrastructure;
using Domain;

namespace MvcApplication.Controllers
{
    public class HomeController : BaseController
    {
        int pageSize = 4;

        public ActionResult Index(int userID = 0, int page = 1)
        {
            var obj = new IndexModel();
            obj.CurrentUser = core.ProfileRepository.Find(userID);
            obj.IndexPagingInfo = new PagingInfo()
               {
                   CurrentPage = page,
                   ItemsPerPage = pageSize,
                   TotalItems = core.PictureRepository.ReadAll().Count(),
               };
            return View(obj);
        }

        public PartialViewResult GetPictureData(int current_page, int current_user_id)
        {
            var page = current_page;
            var model = new GetPictureDataModel()
            {
                Pictures = core.PictureRepository.ReadAll()
                            .OrderByDescending(x => x.ID)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize).ToList(),
                CurrentUser = core.ProfileRepository.Find(current_user_id),
                CurrentPage = current_page,
            };
            return PartialView(model);
        }

        public ActionResult AddToHeartedPics(int userID, int page, int pictureID)
        {
            core.ProfileRepository.Find(userID).HeartedPics.Add(core.PictureRepository.Read(pictureID));
            core.Submit();
            return RedirectToRoute("Index", new { userID = userID, page = page });
        }

        public ActionResult DeleteFromHeartedPics(int userID, int page, int pictureID, int fromIndex = 1)
        {
            core.ProfileRepository.Find(userID).HeartedPics.Remove(core.PictureRepository.Read(pictureID));
            core.Submit();
            if (fromIndex == 1)
                return RedirectToRoute("Index", new { userID = userID, page = page });
            else return RedirectToRoute("ProfilePageLikes", new { userID = userID, tagsPage = page });
        }

        public ActionResult ManageTags() 
        {
            string apiUri = Url.HttpRouteUrl("TagAPI", new { controller = "TagWebAPI"});
            ViewBag.ApiUrl = new Uri(Request.Url, apiUri).AbsoluteUri.ToString();
            return View();
        }

    }
}
