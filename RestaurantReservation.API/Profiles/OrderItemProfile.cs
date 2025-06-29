﻿using AutoMapper;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemCreationDto, OrderItem>();
            CreateMap<OrderItemUpdatedDto, OrderItem>().ReverseMap(); ;

        }
    }
}
