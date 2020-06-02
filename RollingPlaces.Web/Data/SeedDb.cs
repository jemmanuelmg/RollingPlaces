using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RollingPlaces.Common.Enums;
using RollingPlaces.Web.Data.Entities;
using RollingPlaces.Web.Helpers;

namespace RollingPlaces.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext, IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();

            var admin = await CheckUserAsync("Emmanuel", "Martinez", "3182024296", "jemmanuelmg@hotmail.com", UserType.Admin);
            var user1 = await CheckUserAsync("Maria", "Arenas", "3182024296", "maria@yopmail.com", UserType.User);
            var user2 = await CheckUserAsync("Carlos", "Restrepo", "3182024296", "carlos@yopmail.co", UserType.User);

            var city1 = await CheckCity("Medellin");
            var city2 = await CheckCity("Bogotá");
            var city3 = await CheckCity("Cali");
            var city4 = await CheckCity("Barranquilla");
            var city5 = await CheckCity("Cartagena");
            var city6 = await CheckCity("Pereira");
            var city7 = await CheckCity("Manizales");
            var city8 = await CheckCity("Bucaramanga");

            var category1 = await CheckCategory("Turístico");
            var category2 = await CheckCategory("Restaurante");
            var category3 = await CheckCategory("Almacén");
            var category4 = await CheckCategory("Comidas Rápidas");
            var category5 = await CheckCategory("Centro de Servicios");
            var category6 = await CheckCategory("Centro Comercial");
            var category7 = await CheckCategory("Pasaje Comercial");
            var category15 = await CheckCategory("Local Comercial");
            var category8 = await CheckCategory("Parada de Autobus");
            var category10 = await CheckCategory("Variedades");
            var category11 = await CheckCategory("Salón de Belleza");
            var category12 = await CheckCategory("Barbería");
            var category13 = await CheckCategory("Tienda");
            var category14 = await CheckCategory("Oferta de Servicios");


            await CheckPlacesAsync(admin, user1, user2, city1, city2, city3, category2, category5, category12, category10);
        }

        private async Task<UserEntity> CheckUserAsync(string firstName, string lastName, string phone, string email, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    UserType = userType,
                    LoginType = LoginType.RollingPlaces
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<CityEntity> CheckCity(string cityName)
        {
            CityEntity city = new CityEntity();
            city.Name = cityName;
            _dataContext.Add(city);
            await _dataContext.SaveChangesAsync();

            return city;

        }

        private async Task<CategoryEntity> CheckCategory(string categoryName)
        {
            CategoryEntity category = new CategoryEntity();
            category.Name = categoryName;
            _dataContext.Add(category);
            await _dataContext.SaveChangesAsync();

            return category;

        }

        private async Task CheckPlacesAsync(UserEntity user1, UserEntity user2, UserEntity user3, CityEntity city1, CityEntity city2, CityEntity city3, CategoryEntity category1, CategoryEntity category2, CategoryEntity category3, CategoryEntity category4)
        {
            if (!_dataContext.Places.Any())
            {
                _dataContext.Places.Add(new PlaceEntity
                {
                    Name = "Restaurante Doris Bill",
                    CreatedDate = DateTime.Now,
                    Description = "Restaurante Doris Bill desde 1989. Los mejores almuerzos al mejor precio. Horario de atención: Lunes a Sabado de 8:00 am a 9:00 pm. Domingos y festivos: 8:00 am a 12:00 m. Servicio a Domicilio",
                    Latitude = -179.1234567891982,
                    Longitude = 234.1234567891827,
                    Category = category1,
                    City = city1,
                    User = user1,
                    Qualifications = new List<QualificationEntity>
                    {
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 4,
                            Comment = "Es un excelente restaurante, muy buena atención y buena comida. Me gustaría ver mas variadad en el menu pero me gusto mucho"
                        },
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 5,
                            Comment = "Buena atención y preparan los platos sin demora."
                        }
                    },
                    Photos = new List<PhotoEntity>
                    {
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        },
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        }
                    }

                });

                _dataContext.Places.Add(new PlaceEntity
                {
                    Name = "Cafe Internet La Floresta",
                    CreatedDate = DateTime.Now,
                    Description = "Cafe Internet y centro de Servicios La Floresta. Pague facturas de todo tipo, servicios públicos, lotería, recargas, apuestas deportivas y giros. Horario de atención: Lunes a Sabado de 8:00 am a 9:00 pm. Domingos y festivos: 8:00 am a 12:00 m. Servicio a Domicilio",
                    Latitude = 180.1234567891982,
                    Longitude = 210.1234567891827,
                    Category = category2,
                    City = city1,
                    User = user1,
                    Qualifications = new List<QualificationEntity>
                    {
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 4,
                            Comment = "Es un excelente cafe internet, atienden bien"
                        },
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 2,
                            Comment = "El lugar es muy sucio y se escucha mucho ruido"
                        }
                    },
                    Photos = new List<PhotoEntity>
                    {
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        },
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        }
                    }

                });

                _dataContext.Places.Add(new PlaceEntity
                {
                    Name = "Barbería London City",
                    CreatedDate = DateTime.Now,
                    Description = "Barbería London City, la mejor barbería del barrio Laureles. Cortes para todos los gustos y al mejor precio. Horario de atención: Lunes a Sabado de 8:00 am a 9:00 pm. Domingos y festivos: 8:00 am a 12:00 m.",
                    Latitude = 160.1234567891982,
                    Longitude = 120.1234567891827,
                    Category = category3,
                    City = city1,
                    User = user2,
                    Qualifications = new List<QualificationEntity>
                    {
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 3,
                            Comment = "Una buena barbería, aconsejaría no hacer tanto ruido con la musica al momento de atender clientes"
                        },
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 5,
                            Comment = "Es una buena barbería, la antención es amable, tienen buenos precios y el lugar es limpio"
                        }
                    },
                    Photos = new List<PhotoEntity>
                    {
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        },
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        }
                    }

                });

                _dataContext.Places.Add(new PlaceEntity
                {
                    Name = "Variadades El Tesoro de la 80",
                    CreatedDate = DateTime.Now,
                    Description = "Tienda y Variadades El Tesoro de la 80, encuentre toda clase de productos: aseo personal y del hogar, papelería, piñatería, decoración, relojes, bolsos, juguetería y mucho mas. Horario de atención: Lunes a Sabado de 8:00 am a 9:00 pm. Domingos y festivos: 8:00 am a 12:00 m.",
                    Latitude = 140.1234567891982,
                    Longitude = 150.1234567891827,
                    Category = category3,
                    City = city1,
                    User = user2,
                    Qualifications = new List<QualificationEntity>
                    {
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 2,
                            Comment = "Tienen buena variedad de productos pero la atención es pésima, se demoran mucho en atender a las personas"
                        },
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 3,
                            Comment = "Los productos tienen mala calidad"
                        }
                    },
                    Photos = new List<PhotoEntity>
                    {
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        },
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        }
                    }

                });

                _dataContext.Places.Add(new PlaceEntity
                {
                    Name = "HeyCoffe",
                    CreatedDate = DateTime.Now,
                    Description = "Restaurante Doris Bill desde 1989. Los mejores almuerzos al mejor precio. Horario de atención: Lunes a Sabado de 8:00 am a 9:00 pm. Domingos y festivos: 8:00 am a 12:00 m. Servicio a Domicilio",
                    Latitude = -179.1234567891982,
                    Longitude = 234.1234567891827,
                    Category = category1,
                    City = city1,
                    User = user1,
                    Qualifications = new List<QualificationEntity>
                    {
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 4,
                            Comment = "Es un excelente restaurante, muy buena atención y buena comida. Me gustaría ver mas variadad en el menu pero me gusto mucho"
                        },
                        new QualificationEntity
                        {
                            CreatedDate = DateTime.Now,
                            User = user1,
                            Value = 5,
                            Comment = "Buena atención y preparan los platos sin demora."
                        }
                    },
                    Photos = new List<PhotoEntity>
                    {
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        },
                        new PhotoEntity
                        {
                            PhotoPath = "images/place-photos/120924993.jpg"
                        }
                    }

                });

                await _dataContext.SaveChangesAsync();

            }
        }
    }
}


