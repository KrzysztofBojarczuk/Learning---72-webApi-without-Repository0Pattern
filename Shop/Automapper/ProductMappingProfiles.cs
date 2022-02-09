using AutoMapper;
using Shop.Dttos;
using Shop.Models;

namespace Shop.Automapper
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductGetDto>();
        }
    }
}
