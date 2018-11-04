using HLTAT.Business.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HLTAT.Business.Factory
{
    public interface IProductFactory
    {
        void AddProduct(ProductsModel prod);
        void DeleteProduct(ProductsModel prod);
        IQueryable<ProductsModel> GetProducts(Expression<Func<ProductsModel, bool>> exp);
        Task<int> SaveChangesAsync();
        void UpdateProduct(ProductsModel prod);
    }
}