using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public interface ISalesHistoryService
    {
        IEnumerable<SaleHistoryModel> GetSalesByPartner(int partnerId);
    }
}