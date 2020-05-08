using System.ComponentModel.DataAnnotations;

namespace RollingPlaces.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
