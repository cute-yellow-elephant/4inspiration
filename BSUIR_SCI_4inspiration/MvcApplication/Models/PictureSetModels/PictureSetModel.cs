using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApplication.Models.PictureSetModels
{
    public class PictureSetModel
    {
        [Required(ErrorMessage = "Please enter the name, the field is empty (")]
        [StringLength(14, MinimumLength = 3, ErrorMessage = "Please enter a valid name of the set, just look at the restrictions")]
        [Display(Name = "Name of the set (14 symbols max, 3 min):")]
        public virtual string Name { get; set; }

        public byte[] CoverData { get; set; }
        public string CoverMimeType { get; set; }
    }
}