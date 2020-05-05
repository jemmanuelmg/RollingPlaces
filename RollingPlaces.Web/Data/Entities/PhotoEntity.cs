using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollingPlaces.Web.Data.Entities
{
    public class PhotoEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PlaceEntity Place { get; set; }

        [Display(Name = "Photo Path")]
        public String PhotoPath { get; set; }

        public String Description { get; set; }

    }
}
