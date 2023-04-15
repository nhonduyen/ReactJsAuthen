using AutoMapper;
using Odering.Core.Entities;
using Ordering.Application.Commands.Customers.Create;
using Ordering.Application.Commands.Customers.Update;
using Ordering.Application.DTOs;

namespace Ordering.Application.Mapper
{
    public class OrderingMappingProfile : Profile
    {
        public OrderingMappingProfile()
        {
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, EditCustomerCommand>().ReverseMap();
        }
    }
}
