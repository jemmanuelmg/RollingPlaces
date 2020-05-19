using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollingPlaces.Web.Data.Entities
{
    public class QualificationEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int Value { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime CreatedDate { get; set; }

        public UserEntity User { get; set; }

        public PlaceEntity Place { get; set; }

    }
}
