using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RollingPlaces.Common.Enums;
using RollingPlaces.Web.Data.Entities;

namespace RollingPlaces.Web.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public string FullNameWithDocument => $"{FirstName} {LastName}";

        public ICollection<PlaceEntity> Places{ get; set; }

        public ICollection<QualificationEntity> Qualifications { get; set; }

    }
}

