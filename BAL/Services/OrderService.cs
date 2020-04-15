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
    public class OrderService : IOrderService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public OrderService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO order)
        {
            var addedOrder = _unitOfWork.Orders.CreateAsync(_mapper.Map<OrderDTO, Order>(order));
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Order, OrderDTO>(addedOrder);
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Orders.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<Order, OrderDTO>(await _unitOfWork.Orders.GetByIdAsync(id));
        }

        public async Task UpdateAsync(int id, OrderDTO order)
        {
            order.Id = id;
            _unitOfWork.Orders.Update(_mapper.Map<OrderDTO, Order>(order));
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsOfOrderAsync(int orderId)
        {
            var result = new List<Product>();
            var orderProducts = await _unitOfWork.OrderProducts.GetAllAsync(x => x.OrderId == orderId);

            foreach (var op in orderProducts)
            {
                result.Add(await _unitOfWork.Products.GetByIdAsync(op.ProductId));
            }

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(result);
        }
    }
}

