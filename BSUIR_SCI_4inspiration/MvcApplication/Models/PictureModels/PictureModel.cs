using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace MvcApplication.Models.PictureModels
{
    public class PictureModel
    {
        [Display(Name = "Name of the picture (min 3, max 14 symbols):")]
        [Required(ErrorMessage = "Please enter the name of the picture")]
        [StringLength(14, MinimumLength = 3, ErrorMessage = "Please enter a valid name of the picture")]
        public string Name { get; set; }
        [Display(Name = "Description of the picture (min 3, max 200):")]
        [StringLength(14, MinimumLength = 3, ErrorMessage = "Please enter a valid description of the picture")]
        [Required(ErrorMessage = "Several lines about the picture in description, please")]
        public string ShortInfo { get; set; }

        [Display(Name = "Enter at least one tag in here (max 10 tags, separate them by ','):")]
        [Required(ErrorMessage = "Tags, please")]
        public string TagsLine { get; set; }
        
        public byte[] PictureData { get; set; }
        public string PictureMimeType { get; set; }
    }
}