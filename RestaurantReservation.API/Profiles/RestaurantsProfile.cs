using AutoMapper;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantCreationDto, Restaurant>();
            CreateMap<RestaurantUpdatedDto, Restaurant>().ReverseMap(); ;
        }
    }
}
