using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.dbContext;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public class SalesHistoryService : ISalesHistoryService
    {
        public IEnumerable<SaleHistoryModel> GetSalesByPartner(int partnerId)
        {
            using (var db = DbContextFactory.Create())
            {
                return db.SalesHistory
                    .Where(sh => sh.PartnerID == partnerId)
                    .Select(sh => new SaleHistoryModel
                    {
                        SaleId = sh.SaleID,
                        ProductId = sh.ProductID,
                        ProductName = sh.Products.ProductName,
                        Quantity = sh.Quantity,
                        SaleDate = sh.SaleDate,
                        TotalAmount = sh.Quantity * sh.Products.Price
                    })
                    .ToList();
            }
        }
    }
}