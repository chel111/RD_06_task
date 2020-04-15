using System;
using System.Collections.Generic;
using System.Text;
using BAL.DTO;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IOrderService : IService<OrderDTO>
    {
        Task<IEnumerable<ProductDTO>> GetProductsOfOrderAsync(int orderId);

    }
}
