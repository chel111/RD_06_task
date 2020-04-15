using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IService<TEntityDTO> where TEntityDTO: class
    {
        Task<TEntityDTO> CreateAsync(TEntityDTO product);
        Task UpdateAsync(int id, TEntityDTO product);
        Task DeleteAsync(int id);
        Task<TEntityDTO> GetByIdAsync(int id);
        Task<IEnumerable<TEntityDTO>> GetAllAsync();

    }
}
