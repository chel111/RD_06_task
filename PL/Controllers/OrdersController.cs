using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using PL.Models;
using BAL.Interfaces;
using BAL.DTO;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private IOrderService _orderService;

        public OrdersController(ILogger<ProductsController> logger, IMapper mapper, IOrderService orderService)
        {
            _logger = logger;
            _mapper = mapper;
            _orderService = orderService;
        }


        [HttpGet]
        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            var orderDTOs = await _orderService.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDTO>, IEnumerable<OrderModel>>(orderDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return _mapper.Map<OrderDTO, OrderModel>(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> CreateOrder(OrderModel order)
        {
            var newOrder = await _orderService.CreateAsync(_mapper.Map<OrderModel, OrderDTO>(order));
            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id },
                _mapper.Map<OrderDTO, OrderModel>(newOrder));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> UpdateProduct(int id, OrderModel orderModel)
        {
            if (await _orderService.GetByIdAsync(id) == null)
            {
                return await CreateOrder(orderModel);
            }
            await _orderService.UpdateAsync(id, _mapper.Map<OrderModel, OrderDTO>(orderModel));
            return Ok();
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsOfOrder(int id)
        {
            var products = await _orderService.GetProductsOfOrderAsync(id);
            return Ok(_mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductModel>>(products));
        }
    }
}