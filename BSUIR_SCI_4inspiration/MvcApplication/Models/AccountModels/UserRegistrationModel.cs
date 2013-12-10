using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApplication.Models.AccountModels
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Please enter your login")]
        [RegularExpression(@"\w{3,12}", ErrorMessage = "Please enter a valid login")]
        [Display(Name = "Login (12 symbols max, 3 min, just letters and figures):")]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [RegularExpression(@"\w{5,10}", ErrorMessage = "Please enter a valid password")]
        [Display(Name = "Password (10 symbols max, 5 min):")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessage = "Passwords are not equal")]
        [Display(Name = "Confirm password:")]
        [DataType(DataType.Password)]
        public virtual string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email:")]
        public string Email { set; get; }

    }
}