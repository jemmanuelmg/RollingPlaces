using System;
using System.Collections.Generic;
using System.Text;

namespace RollingPlaces.Common.Models
{
    public class QualificationResponse
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedDate { get; set; }

        public UserResponse User { get; set; }
    }
}
