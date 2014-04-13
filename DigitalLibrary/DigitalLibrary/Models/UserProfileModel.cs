using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    [Serializable]
    public class UserProfileModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessageResourceType = (typeof(Localization.Error)), ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Localization.Error)), ErrorMessageResourceName = "LastNameRequired")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessageResourceType = (typeof(Localization.Error)), ErrorMessageResourceName = "EmailRequired")]
        public string Email { get; set; }

        [Display(Name = "Profile image")]
        public HttpPostedFileBase Image { get; set; }
    }
}