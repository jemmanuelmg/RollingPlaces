using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollingPlaces.Web.Data.Entities
{
    public class PlaceEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<QualificationEntity> Qualifications { get; set; }

        public ICollection<PhotoEntity> Photos { get; set; }

        public CategoryEntity Category { get; set; }

        public CategoryEntity City { get; set; }

    }
}
