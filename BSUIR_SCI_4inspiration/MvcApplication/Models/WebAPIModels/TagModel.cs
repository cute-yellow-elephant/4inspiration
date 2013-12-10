using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain;
using Newtonsoft.Json;

namespace MvcApplication.Models.WebAPIModels
{
    public class TagModel
    {
        [ScaffoldColumn(false)]
        public int ID { set; get; }
        [Required, StringLength(20), MinLength(3)]
        public string Name { set; get; }
        [JsonIgnore]
        public virtual ICollection<Picture> Pictures { set; get; }
    }
}