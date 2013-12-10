using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using MvcApplication.Infrastructure;

namespace MvcApplication.Models.AccountModels
{
    public class ProfilePageModel
    {
        public Profile ViewedUser { get; set; }
        public string UserLogin { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string PersonalInfo { get; set; }
        public string Country { set; get; }
        public string Language { set; get; }
        public string AccountsLinks { set; get; }
        public bool WeeklyNewsletter { set; get; }
        public int FollowersNumber { set; get; }
        public ICollection<Tag> Tags { set; get; }
        public ICollection<Profile> Following { set; get; }
        public ICollection<Picture> HeartedPics { set; get; }
        public ICollection<PictureSet> PictureSets { set; get; }

        public PagingInfo TagsPagingInfo { get; set; }
        public PagingInfo SetsPagingInfo { get; set; }
        public PagingInfo PicsPagingInfo { get; set; }

    }
}