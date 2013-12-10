using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models.AccountModels;
using Domain;
using AppCore;
using MvcApplication.Infrastructure;

namespace MvcApplication.Controllers
{
    public class AccountController : BaseController
    {
        int TagsPageSize = 200;
        int SetsPageSize = 6;
        int PicsPageSize = 6;

        [HttpGet]
        public ActionResult SignIn()
        {
            ViewBag.NoSuchLoginOrEmail = null;
            ViewBag.PasswordIsWrong = null;
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserSignInModel obj)
        {
            ViewBag.PasswordIsWrong = null;
            var userByEmail = core.UserRepository.FindByEmail(obj.LoginEmail);
            var userByLogin = core.UserRepository.Find(obj.LoginEmail);
            if ((userByLogin == null)&&(userByEmail == null))
            {
                ViewBag.NoSuchLoginOrEmail = "Sorry, we haven't managed to find the user with such a login or an email. Try again ^_^";
                return View(obj);
            }
            var user = userByEmail ?? userByLogin;
            if (user.Password != obj.Password.GetHashCode())
            {
                ViewBag.PasswordIsWrong = "The password you entered is wrong";
                return View(obj);
            }
            if (ModelState.IsValid)
                return RedirectToRoute("ProfilePage", new { userID = user.ID} );
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.ErrorLoginIsRegistered = null;
            ViewBag.ErrorEmailIsRegistered = null;
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegistrationModel obj)
        {
            if (core.UserRepository.Find(obj.Login) != null)
            {
                ViewBag.ErrorLoginIsRegistered = "Enter another login, this one is already in use";
                if (core.UserRepository.IsEmailRegistered(obj.Email))
                    ViewBag.ErrorEmailIsRegistered = "Enter another email, this one is already in use";
                return View();
            }
            if (ModelState.IsValid)
            {
                if (core.UserRepository.Find(obj.Login) == null)
                {
                   core.UserRepository.Create(obj.Login, obj.Email, obj.Password.GetHashCode(), DateTime.UtcNow, DateTime.UtcNow);
                   core.RoleRepository.Find("User").Users.Add(core.UserRepository.Find(obj.Login));
                   core.Submit();
                    return RedirectToRoute("ProfileCreation", new { userID = core.UserRepository.Find(obj.Login).ID, userLogin = obj.Login });
                }
            }  
            return View();
        }

        [HttpGet]
        public ActionResult CreateProfile(int userID, string userLogin)
        {
            ViewBag.UserLogin = userLogin;
            ViewBag.UserID = userID;
            ViewBag.Countries = core.CountryRepository.ReadAll();
            ViewBag.Languages = core.LanguageRepository.ReadAll();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProfile(ProfileModificationModel obj, HttpPostedFileBase image, int userID, string userLogin)
        {
            obj.Countries = core.CountryRepository.ReadAll();
            obj.Languages = core.LanguageRepository.ReadAll();
            ViewBag.UserLogin = userLogin;
            ViewBag.UserID = userID;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    obj.AvatarMimeType = image.ContentType;
                    obj.AvatarData = new byte[image.ContentLength];
                    image.InputStream.Read(obj.AvatarData, 0, image.ContentLength);
                }
                core.ProfileRepository.Create(userID, obj.Name, obj.PersonalInfo, Convert.ToInt32(obj.LanguageID), Convert.ToInt32(obj.CountryID), obj.AccountsLinks, obj.WeeklyNewsletter, obj.AvatarData, obj.AvatarMimeType);
                core.PictureSetRepository.Create("Uncathegorized", DateTime.UtcNow, null, "", core.ProfileRepository.Find(userID).ID);
                return RedirectToRoute("ProfilePage", new { userID = userID});
            }
            else
                return View(obj);
        }

        public ActionResult ProfilePage(int userID, int viewedUserID = 0, int tagsPage = 1, int setsPage = 1)//options
        {
            return View(this.GetInitializedPPModel(userID, tagsPage, setsPage, viewedUserID));
        }

        public ActionResult ProfilePageSets(int userID,  int viewedUserID = 0, int tagsPage = 1, int setsPage = 1)
        {
            return View(this.GetInitializedPPModel(userID, tagsPage, setsPage, viewedUserID));
        }

        public ActionResult ProfilePageLikes(int userID,  int viewedUserID = 0, int tagsPage = 1, int likesPage = 1)
        {
            var profile_page = this.GetInitializedPPModel(userID, tagsPage, 1, viewedUserID);

            profile_page.PicsPagingInfo = new PagingInfo()
            {
                CurrentPage = likesPage,
                ItemsPerPage = PicsPageSize,
                TotalItems = profile_page.HeartedPics.Count()
            };

            return View(profile_page);      
        }

        public ActionResult ProfilePageViewSet(int userID, int pictureSetID, int tagsPage = 1, int picsPage = 1, int viewedUserID = 0)
        {
            var profile_page = this.GetInitializedPPModel(userID, tagsPage, 1, viewedUserID);
            ViewBag.PictureSetID = pictureSetID;

            profile_page.PicsPagingInfo = new PagingInfo()
                {
                    CurrentPage = picsPage,
                    ItemsPerPage = PicsPageSize,
                    TotalItems = core.PictureSetRepository.Read(pictureSetID).Pictures.Count()
                };

            return View(profile_page);
        }

        public ProfilePageModel GetInitializedPPModel(int userID, int tagsPage, int setsPage, int viewedUserID)
        {
            var user = core.UserRepository.Find(userID);
            var profile = core.ProfileRepository.Find(userID);
            var viewed_user = core.ProfileRepository.Find(viewedUserID);
            ProfilePageModel profile_model;

            if (viewed_user != null)
                profile_model = new ProfilePageModel()
                {
                    ViewedUser = viewed_user,
                    UserLogin = core.UserRepository.Find(viewed_user.UserID).Login,
                    UserID = userID,
                    Name = viewed_user.Name,
                    PersonalInfo = viewed_user.PersonalInfo,
                    WeeklyNewsletter = viewed_user.WeeklyNewsletter,
                    Language = viewed_user.Language.Name,
                    Country = viewed_user.Country.Name,
                    AccountsLinks = viewed_user.AccountsLinks,
                    FollowersNumber = core.ProfileRepository.CountFollowers(viewed_user.UserID),
                    Following = viewed_user.Following,

                    Tags = core.ProfileRepository.GetTags(viewed_user.UserID)
                      .OrderBy(p => p.ID)
                      .Skip((tagsPage - 1) * TagsPageSize)
                      .Take(TagsPageSize).ToList(),

                    HeartedPics = viewed_user.HeartedPics,

                    PictureSets = viewed_user.PictureSets
                      .OrderBy(p => p.ID)
                      .Skip((setsPage - 1) * SetsPageSize)
                      .Take(SetsPageSize).ToList(),

                    TagsPagingInfo = new PagingInfo()
                    {
                        CurrentPage = tagsPage,
                        ItemsPerPage = TagsPageSize,
                        TotalItems = core.ProfileRepository.GetTags(viewed_user.UserID).Count()
                    },

                    SetsPagingInfo = new PagingInfo()
                    {
                        CurrentPage = setsPage,
                        ItemsPerPage = SetsPageSize,
                        TotalItems = viewed_user.PictureSets.Count()
                    },

                };
            else
                profile_model = new ProfilePageModel()
                {
                    ViewedUser = null,
                    UserLogin = user.Login,
                    UserID = userID,
                    Name = profile.Name,
                    PersonalInfo = profile.PersonalInfo,
                    WeeklyNewsletter = profile.WeeklyNewsletter,
                    Language = profile.Language.Name,
                    Country = profile.Country.Name,
                    AccountsLinks = profile.AccountsLinks,
                    FollowersNumber = core.ProfileRepository.CountFollowers(profile.UserID),
                    Following = profile.Following,

                    Tags = core.ProfileRepository.GetTags(profile.UserID)
                      .OrderBy(p => p.ID)
                      .Skip((tagsPage - 1) * TagsPageSize)
                      .Take(TagsPageSize).ToList(),

                    HeartedPics = profile.HeartedPics,

                    PictureSets = profile.PictureSets
                      .OrderBy(p => p.ID)
                      .Skip((setsPage - 1) * SetsPageSize)
                      .Take(SetsPageSize).ToList(),

                    TagsPagingInfo = new PagingInfo()
                    {
                        CurrentPage = tagsPage,
                        ItemsPerPage = TagsPageSize,
                        TotalItems = core.ProfileRepository.GetTags(profile.UserID).Count()
                    },

                    SetsPagingInfo = new PagingInfo()
                    {
                        CurrentPage = setsPage,
                        ItemsPerPage = SetsPageSize,
                        TotalItems = profile.PictureSets.Count()
                    },

                };

            return profile_model;
        }

        public FileContentResult GetImage(int id1, int id2, int id3)
        {
            if (id1 != 0)
            {
                var profile = core.ProfileRepository.Find(id1);
                return File(profile.AvatarData, profile.AvatarMimeType);
            }
            if (id2 != 0)
            {
                var picture_set = core.PictureSetRepository.Read(id2);
                return File(picture_set.CoverData, picture_set.CoverMimeType);
            }
            if (id3 != 0)
            {
                var picture = core.PictureRepository.Read(id3);
                return File(picture.PictureData, picture.PictureMimeType);
            }
            return null;
        }

    }
}
