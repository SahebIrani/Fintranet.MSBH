using AutoMapper;

using Domain.DTO;
using Domain.Entities;

namespace BuildingBlocks.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
    }
}
