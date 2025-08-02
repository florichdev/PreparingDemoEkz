using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersApp.Models
{
    public class SaleHistoryModel
    {
        public int SaleId { get; set; }
        public int PartnerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}