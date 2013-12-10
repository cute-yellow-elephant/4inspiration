using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using MvcApplication.Infrastructure;

namespace MvcApplication.Models.HomeModels
{
    public class IndexModel
    {
        public Profile CurrentUser { get; set; }
        public PagingInfo IndexPagingInfo { get; set; }
    }

    public class GetPictureDataModel
    {
        public Profile CurrentUser { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public int CurrentPage { set; get; }
    }
}