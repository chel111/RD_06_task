using System;
using System.Collections.Generic;
using System.Text;
using BAL.DTO;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IProductService : IService<ProductDTO>
    {

        Task AddProductToOrderAsync(int productId, int orderId);
    }
}
