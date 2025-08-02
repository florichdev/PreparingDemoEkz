using System.Collections.Generic;
using System.Linq;
using PartnersApp.dbContext;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public class ProductService : IProductService
    {
        public IEnumerable<ProductModel> GetAllProducts()
        {
            using (var db = DbContextFactory.Create())
            {
                return db.Products.Select(p => new ProductModel
                {
                    ProductId = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.Price
                }).ToList();
            }
        }

        public ProductModel GetProductById(int id)
        {
            using (var db = DbContextFactory.Create())
            {
                var product = db.Products.Find(id);
                return product == null ? null : new ProductModel
                {
                    ProductId = product.ProductID,
                    ProductName = product.ProductName,
                    Price = product.Price
                };
            }
        }

        public void AddProduct(ProductModel product)
        {
            using (var db = DbContextFactory.Create())
            {
                db.Products.Add(new Products
                {
                    ProductName = product.ProductName,
                    Price = product.Price
                });
                db.SaveChanges();
            }
        }

        public void UpdateProduct(ProductModel product)
        {
            using (var db = DbContextFactory.Create())
            {
                var entity = db.Products.Find(product.ProductId);
                if (entity != null)
                {
                    entity.ProductName = product.ProductName;
                    entity.Price = product.Price;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (var db = DbContextFactory.Create())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
        }
    }
}