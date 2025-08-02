using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public interface IPartnerService
    {
        IEnumerable<PartnerModel> GetAllPartners();
        PartnerModel GetPartnerById(int id);
        void AddPartner(PartnerModel partner);
        void UpdatePartner(PartnerModel partner);
        void DeletePartner(int id);
        decimal GetTotalSales(int partnerId);
        int CalculateDiscount(decimal totalSales);
    }
}