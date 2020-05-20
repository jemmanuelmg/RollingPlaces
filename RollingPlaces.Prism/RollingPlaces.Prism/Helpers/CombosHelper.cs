using System;
using System.Collections.Generic;
using System.Text;
using RollingPlaces.Common.Models;

namespace RollingPlaces.Prism.Helpers
{
    public static class CombosHelper
    {

        public static List<Comment> GetComments()
        {
            return new List<Comment>
            {
                new Comment { Id = 1, Name = Languages.Comment1 },
                new Comment { Id = 2, Name = Languages.Comment2 },
                new Comment { Id = 3, Name = Languages.Comment3 },
                new Comment { Id = 4, Name = Languages.Comment4 },
                new Comment { Id = 5, Name = Languages.Comment5 },
                new Comment { Id = 6, Name = Languages.Comment6 },
                new Comment { Id = 7, Name = Languages.Comment7 },
                new Comment { Id = 8, Name = Languages.Comment8 },
                new Comment { Id = 9, Name = Languages.Comment9 },
                new Comment { Id = 10, Name = Languages.Comment10 }
            };
        }

        public static List<PlaceCategory> GetPlaceCategories()
        {
            return new List<PlaceCategory>
            {
                new PlaceCategory { Id = 777, Name = "Todas las categorías" },
                new PlaceCategory { Id = 1, Name = "Turístico" },
                new PlaceCategory { Id = 2, Name = "Restaurante" },
                new PlaceCategory { Id = 3, Name = "Almacén" },
                new PlaceCategory { Id = 4, Name = "Comidas Rápidas" },
                new PlaceCategory { Id = 5, Name = "Centro de Servicios" },
                new PlaceCategory { Id = 6, Name = "Centro Comercial" },
                new PlaceCategory { Id = 7, Name = "Pasaje Comercial" },
                new PlaceCategory { Id = 8, Name = "Local Comercial" },
                new PlaceCategory { Id = 9, Name = "Parada de Autobus" },
                new PlaceCategory { Id = 10, Name = "Variedades" },
                new PlaceCategory { Id = 11, Name = "Salón de Belleza" },
                new PlaceCategory { Id = 12, Name = "Barbería" },
                new PlaceCategory { Id = 13, Name = "Tienda" },
                new PlaceCategory { Id = 14, Name = "Oferta de Servicios" }
            };
        }

        public static List<PlaceCategory> GetPlaceCategories2()
        {
            return new List<PlaceCategory>
            {
                new PlaceCategory { Id = 1, Name = "Turístico" },
                new PlaceCategory { Id = 2, Name = "Restaurante" },
                new PlaceCategory { Id = 3, Name = "Almacén" },
                new PlaceCategory { Id = 4, Name = "Comidas Rápidas" },
                new PlaceCategory { Id = 5, Name = "Centro de Servicios" },
                new PlaceCategory { Id = 6, Name = "Centro Comercial" },
                new PlaceCategory { Id = 7, Name = "Pasaje Comercial" },
                new PlaceCategory { Id = 8, Name = "Local Comercial" },
                new PlaceCategory { Id = 9, Name = "Parada de Autobus" },
                new PlaceCategory { Id = 10, Name = "Variedades" },
                new PlaceCategory { Id = 11, Name = "Salón de Belleza" },
                new PlaceCategory { Id = 12, Name = "Barbería" },
                new PlaceCategory { Id = 13, Name = "Tienda" },
                new PlaceCategory { Id = 14, Name = "Oferta de Servicios" }
            };
        }

        public static List<PlaceCity> GetPlaceCities()
        {
            return new List<PlaceCity>
            {
                new PlaceCity { Id = 1, Name = "Medellin" },
                new PlaceCity { Id = 2, Name = "Bogotá" },
                new PlaceCity { Id = 3, Name = "Cali" },
                new PlaceCity { Id = 4, Name = "Barranquilla" },
                new PlaceCity { Id = 5, Name = "Cartagena" },
                new PlaceCity { Id = 6, Name = "Pereira" },
                new PlaceCity { Id = 7, Name = "Manizales" },
                new PlaceCity { Id = 8, Name = "Bucaramanga" }
            };
        }

    }
}
