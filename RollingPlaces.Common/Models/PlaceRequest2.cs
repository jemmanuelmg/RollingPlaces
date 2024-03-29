﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RollingPlaces.Common.Models
{
    public class PlaceRequest2
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Guid User { get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public byte[] PictureArray1 { get; set; }

        public byte[] PictureArray2 { get; set; }

        public byte[] PictureArray3 { get; set; }

        public byte[] PictureArray4 { get; set; }

    }
}
