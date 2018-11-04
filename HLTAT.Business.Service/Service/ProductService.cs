using HLTAT.Business.Factory;
using HLTAT.Business.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HLTAT.Business.Service
{
    public class ProductService : IProductService
    {
        private IProductFactory _prodFty;
        public ProductService(IProductFactory prodFactory) => _prodFty = prodFactory;


        public async Task<ProductsModel> GetProduct(Guid id, DateTime timestamp, long productId)
        {
            return await _prodFty
                 .GetProducts(x => x.ID == id && x.Timestamp == timestamp && x.Products.FirstOrDefault().ID == productId)
                 .Include(x => x.Products)
                 .FirstOrDefaultAsync();
        }

        public async void AddOrUpdateProduct(ProductsModel prod)
        {
            var existProd = await GetProduct(prod.ID, prod.Timestamp, prod.Products.FirstOrDefault().ID);

            if (existProd == null)
            {
                prod.ID = Guid.NewGuid();
                prod.Timestamp = DateTime.UtcNow;
                _prodFty.AddProduct(prod);
            }
            else
            {
                prod.Timestamp = existProd.Timestamp;
                prod.Products.FirstOrDefault().ID = prod.Products.FirstOrDefault().ID;
                _prodFty.UpdateProduct(prod);
            }
        }


        public void DeleteProduct(ProductsModel prod) => _prodFty.DeleteProduct(prod);


        public async Task<int> SaveChangesAsync() => await _prodFty.SaveChangesAsync();

    }
}
