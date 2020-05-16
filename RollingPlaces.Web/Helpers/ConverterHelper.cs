using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RollingPlaces.Common.Models;
using RollingPlaces.Web.Data.Entities;

namespace RollingPlaces.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public PlaceResponse ToPlaceResponse(PlaceEntity placeEntity)
        {
            return new PlaceResponse
            {
                Id = placeEntity.Id,
                CreatedDate = placeEntity.CreatedDate,
                Name = placeEntity.Name,
                Description = placeEntity.Description,
                Latitude = placeEntity.Latitude,
                Longitude = placeEntity.Longitude,
                User = ToUserResponse(placeEntity.User),
                Category = ToCategoryResponse(placeEntity.Category),
                City = ToCityResponse(placeEntity.City),
                Qualifications = placeEntity.Qualifications?.Select(q => new QualificationResponse
                {
                    Id = q.Id,
                    Value = q.Value,
                    Comment = q.Comment,
                    CreatedDate = q.CreatedDate,
                    User = ToUserResponse(q.User)

                }).ToList(),
                Photos = placeEntity.Photos?.Select(ph => new PhotoResponse
                {
                    Id = ph.Id,
                    PhotoPath = ph.PhotoPath,
                    Description = ph.Description

                }).ToList(),
            };
        }

        public UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }

        public CategoryResponse ToCategoryResponse(CategoryEntity category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public CityResponse ToCityResponse(CityEntity city)
        {
            if (city == null)
            {
                return null;
            }

            return new CityResponse
            {
                Id = city.Id,
                Name = city.Name
            };
        }

        public QualificationResponse ToQualificationResponse(QualificationEntity qualification)
        {
            if (qualification == null)
            {
                return null;
            }

            return new QualificationResponse
            {
                Id = qualification.Id,
                Value = qualification.Value,
                Comment = qualification.Comment,
                CreatedDate = qualification.CreatedDate,
                User = ToUserResponse(qualification.User)
            };
        }
    }
}
