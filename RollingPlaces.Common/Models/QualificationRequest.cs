using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RollingPlaces.Common.Models
{
   public class QualificationRequest
    {
        [Required]
        public int PlaceId { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
