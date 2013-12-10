using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace MvcApplication.Models.AccountModels
{
    public class ProfileModificationModel
    {
        public string UserLogin { get; set; }
        [Display(Name = "Your name:")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Display(Name = "Any information about youself:")]
        [Required(ErrorMessage = "Several lines about yourself, please")]
        [UIHint("Multiline")]
        public string PersonalInfo { get; set; }
        [Required(ErrorMessage = "Please choose the country you live in")]
        [Display(Name = "Select the country you live in:")]
        public string CountryID { set; get; }
        [Required(ErrorMessage = "Please choose the language you speak")]
        [Display(Name = "Select the language you speak:")]
        public string LanguageID { set; get; }
        [Display(Name = "Links to your accounts anywhere:")]
        [Required(ErrorMessage = "Please, put several links to your accounts anywhere below")]
        [UIHint("Multiline")]
        public string AccountsLinks { set; get; }
        public bool WeeklyNewsletter { set; get; }

        public byte[] AvatarData { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public string AvatarMimeType { get; set; }

        public ICollection<Country> Countries { get; set; }
        public ICollection<Language> Languages { get; set; }
    }
}