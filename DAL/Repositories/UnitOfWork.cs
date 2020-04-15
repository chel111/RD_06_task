using System;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductContext _db;

        private IRepository<Product> _products;
        private IRepository<Order> _orders;
        private IRepository<OrderProduct> _orderProducts;


        public IRepository<Product> Products => _products ?? new Repository<Product>(_db);
        public IRepository<Order> Orders => _orders ?? new Repository<Order>(_db);
        public IRepository<OrderProduct> OrderProducts => _orderProducts ?? new Repository<OrderProduct>(_db);


        public UnitOfWork(ProductContext productContext)
        {
            _db = productContext;
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
