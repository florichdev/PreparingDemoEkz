using PartnersApp.dbContext;
using PartnersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartnersApp.Services
{
    public class Service : IDisposable
    {
        private PartnersDBEntities _db = new PartnersDBEntities();

        public bool Authenticate(string login, string password, out bool isAdmin)
        {
            var user = _db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            isAdmin = user?.IsAdmin ?? false;
            return user != null;
        }

        public List<Partner> GetPartners() => _db.Partners.Select(p => new Partner
        {
            Id = p.PartnerID,
            Name = p.PartnerName,
            TypeId = p.PartnerTypeID,
            TypeName = p.PartnerTypes.TypeName,
            Director = p.DirectorName,
            Phone = p.Phone,
            Email = p.Email,
            Address = p.Address,
            Rating = p.Rating
        }).ToList();

        public void SavePartner(Partner partner)
        {
            var entity = partner.Id == 0 ? new Partners() : _db.Partners.Find(partner.Id);
            if (partner.Id == 0) _db.Partners.Add(entity);

            entity.PartnerName = partner.Name;
            entity.PartnerTypeID = partner.TypeId;
            entity.DirectorName = partner.Director;
            entity.Phone = partner.Phone;
            entity.Email = partner.Email;
            entity.Address = partner.Address;
            entity.Rating = partner.Rating;

            _db.SaveChanges();
        }

        public void DeletePartner(int id)
        {
            var partner = _db.Partners.Find(id);
            if (partner != null) _db.Partners.Remove(partner);
            _db.SaveChanges();
        }

        public List<Product> GetProducts() => _db.Products.Select(p => new Product
        {
            Id = p.ProductID,
            Name = p.ProductName,
            Price = p.Price
        }).ToList();

        public void SaveProduct(Product product)
        {
            var entity = product.Id == 0 ? new Products() : _db.Products.Find(product.Id);
            if (product.Id == 0) _db.Products.Add(entity);

            entity.ProductName = product.Name;
            entity.Price = product.Price;

            _db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null) _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public List<Sale> GetSales(int partnerId) => _db.SalesHistory
            .Where(sh => sh.PartnerID == partnerId)
            .Select(sh => new Sale
            {
                Id = sh.SaleID,
                PartnerId = sh.PartnerID,
                ProductId = sh.ProductID,
                ProductName = sh.Products.ProductName,
                Date = sh.SaleDate,
                Quantity = sh.Quantity,
                Total = sh.Quantity * sh.Products.Price
            }).ToList();

        public void AddSale(int partnerId, int productId, int quantity)
        {
            _db.SalesHistory.Add(new SalesHistory
            {
                PartnerID = partnerId,
                ProductID = productId,
                Quantity = quantity,
                SaleDate = DateTime.Now
            });
            _db.SaveChanges();
        }

        public decimal GetTotalSales(int partnerId)
        {
            var result = _db.SalesHistory
                .Where(sh => sh.PartnerID == partnerId)
                .Sum(sh => (decimal?)sh.Quantity * sh.Products.Price);
            return result ?? 0;
        }

        public int CalculateDiscount(decimal totalSales)
        {
            if (totalSales < 10000) return 0;
            if (totalSales < 50000) return 5;
            if (totalSales < 300000) return 10;
            return 15;
        }

        public void Dispose() => _db?.Dispose();
    }
}