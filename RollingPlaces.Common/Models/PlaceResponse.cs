using System;
using System.Collections.Generic;
using System.Text;

namespace RollingPlaces.Common.Models
{
    public class PlaceResponse
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<QualificationResponse> Qualifications { get; set; }

        public ICollection<PhotoResponse> Photos { get; set; }

        public UserResponse User { get; set; }

        public CategoryResponse Category { get; set; }

        public CityResponse City { get; set; }
    }
}
