using System;
using System.Collections.Generic;
using System.Text;

namespace RollingPlaces.Common.Models
{
    public class PlaceRequest
    {
        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public String Keywords { get; set; }
    }
}
