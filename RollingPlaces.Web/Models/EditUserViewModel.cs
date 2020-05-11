using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RollingPlaces.Web.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Picture")]
        public IFormFile PictureFile { get; set; }

        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
    }
}
