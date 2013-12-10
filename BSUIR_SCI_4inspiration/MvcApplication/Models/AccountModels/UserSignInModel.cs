using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApplication.Models.AccountModels
{
    public class UserSignInModel
    {
        [Required(ErrorMessage = "Please enter your login or email address")]
        [Display(Name = "Login or email:")]
        public virtual string LoginEmail { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
    }
}