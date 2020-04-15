using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text;
using AutoMapper;
using BAL.DTO;
using BAL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Entities;

namespace BAL.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public ProductService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
        }

        public async Task<ProductDTO> CreateAsync(ProductDTO product)
        {
            var addedProduct = _unitOfWork.Products.CreateAsync(_mapper.Map<ProductDTO, Product>(product));
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Product, ProductDTO>(addedProduct);
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Products.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }


        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<Product, ProductDTO>(await _unitOfWork.Products.GetByIdAsync(id));
        }


        public async Task UpdateAsync(int id, ProductDTO product)
        {
            product.Id = id;
            _unitOfWork.Products.Update(_mapper.Map<ProductDTO, Product>(product));
            await _unitOfWork.SaveAsync();
        }


        public async Task AddProductToOrderAsync(int productId, int orderId)
        {
            _unitOfWork.OrderProducts.CreateAsync(new OrderProduct { ProductId = productId, OrderId = orderId });
            await _unitOfWork.SaveAsync();
        }
    }
 
}
