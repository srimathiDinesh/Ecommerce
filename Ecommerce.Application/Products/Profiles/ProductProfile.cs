using AutoMapper;
using Ecommerce.Application.Products.Commands;
using Ecommerce.Application.Products.Models;
using Ecommerce.Domain;

namespace Ecommerce.Application.Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Commands Profile
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();

            // Queries Profile
            CreateMap<Product, ProductModel>();
        }
    }
}
