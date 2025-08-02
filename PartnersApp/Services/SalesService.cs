using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.dbContext;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public class SalesService
    {
        public void AddSale(int partnerId, int productId, int quantity, DateTime saleDate)
        {
            using (var db = DbContextFactory.Create())
            {
                db.SalesHistory.Add(new SalesHistory
                {
                    PartnerID = partnerId,
                    ProductID = productId,
                    Quantity = quantity,
                    SaleDate = saleDate
                });
                db.SaveChanges();
            }
        }
    }
}
