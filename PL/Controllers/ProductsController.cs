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
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IMapper mapper, IProductService productService)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var productDTOs = await _productService.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductModel>>(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductDTO, ProductModel>(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> CreateProduct(ProductModel product)
        {
            var newProduct = await _productService.CreateAsync(_mapper.Map<ProductModel, ProductDTO>(product));
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id },
                _mapper.Map<ProductDTO, ProductModel>(newProduct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductModel>> UpdateProduct(int id, ProductModel productModel)
        {
            if (await _productService.GetByIdAsync(id) == null)
            {
                return await CreateProduct(productModel);
            }
            await _productService.UpdateAsync(id, _mapper.Map<ProductModel, ProductDTO>(productModel));
            return Ok();
        }

        [HttpPost("{id}/orders/{orderId}")]
        public async Task<IActionResult> AddProductInOrder(int id, int orderId)
        {
            await _productService.AddProductToOrderAsync(id, orderId);
            return Ok();
        }

    }
}
