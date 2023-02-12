using AutoMapper;
using ecommerceApi.DTOs;
using ecommerceApi.Entities;

namespace ecommerceApi.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
