using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.DTO;

namespace Ecommerce.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product -> ProductEntity
            CreateMap<Product, ProductEntity>();

            // CartItemEntity -> CartItem
            CreateMap<CartItemEntity, CartItem>();

            // CartItem -> CartItemEntity
            CreateMap<CartItem, CartItemEntity>();


            CreateMap<CartEntity, Cart>();
        }
    }
}