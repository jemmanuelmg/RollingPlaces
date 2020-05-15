using RollingPlaces.Common.Models;
using RollingPlaces.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollingPlaces.Web.Helpers
{
    public interface IConverterHelper
    {
        PlaceResponse ToPlaceResponse(PlaceEntity placeEntity);

        UserResponse ToUserResponse(UserEntity user);

        CategoryResponse ToCategoryResponse(CategoryEntity category);

        CityResponse ToCityResponse(CityEntity city);

        QualificationResponse ToQualificationResponse(QualificationEntity qualification);
    }
}
