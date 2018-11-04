using HLTAT.Business.Model;
using System;
using System.Threading.Tasks;

namespace HLTAT.Business.Service
{
    public interface IProductService
    {
        void AddOrUpdateProduct(ProductsModel prod);
        void DeleteProduct(ProductsModel prod);
        Task<ProductsModel> GetProduct(Guid id, DateTime timestamp, long productId);
        Task<int> SaveChangesAsync();
    }
}