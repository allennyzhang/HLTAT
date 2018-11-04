
using HLTAT.Business.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HLTAT.Business.Factory
{
    public class ProductFactory : IProductFactory
    {
        private ProductContext _prodCtx;
        public ProductFactory(ProductContext prodContext) => _prodCtx = prodContext;

        public IQueryable<ProductsModel> GetProducts(Expression<Func<ProductsModel, bool>> exp) => _prodCtx.ProductsDbSet.AsNoTracking().Where(exp);


        public void AddProduct(ProductsModel prod) => _prodCtx.ProductsDbSet.Add(prod);


        public void UpdateProduct(ProductsModel prod) => _prodCtx.ProductsDbSet.Update(prod);


        public void DeleteProduct(ProductsModel prod) => _prodCtx.ProductsDbSet.Remove(prod);


        public async Task<int> SaveChangesAsync() => await _prodCtx.SaveChangesAsync();
    }
}
