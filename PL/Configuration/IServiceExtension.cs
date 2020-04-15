using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BAL.Interfaces;
using BAL.DTO;
using BAL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using PL.Mapper;
using AutoMapper;
using DAL.EF;

namespace PL.Configuration
{
    public static class IServiceExtension
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //BAL services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //DbContext
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            //mapper
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
