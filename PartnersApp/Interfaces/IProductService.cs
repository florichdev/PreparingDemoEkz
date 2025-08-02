using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();
        ProductModel GetProductById(int id);
        void AddProduct(ProductModel product);
        void UpdateProduct(ProductModel product);
        void DeleteProduct(int id);
    }
}