using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PL.Models;
using BAL.DTO;
using DAL.Entities;

namespace PL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, ProductDTO>();
            CreateMap<ProductDTO, ProductModel>();

            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();

            CreateMap<OrderModel, OrderDTO>();
            CreateMap<OrderDTO, OrderModel>();

            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
