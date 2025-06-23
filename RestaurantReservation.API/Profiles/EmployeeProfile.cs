using AutoMapper;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreationDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap(); ;
        }

    }
}
