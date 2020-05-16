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

    }
}
