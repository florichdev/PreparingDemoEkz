using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.dbContext;
using PartnersApp.Models;

namespace PartnersApp.Services
{
    public class PartnerService : IPartnerService
    {
        public IEnumerable<PartnerModel> GetAllPartners()
        {
            using (var db = DbContextFactory.Create())
            {
                return db.Partners.Select(p => new PartnerModel
                {
                    PartnerId = p.PartnerID,
                    PartnerName = p.PartnerName,
                    PartnerTypeId = p.PartnerTypeID,
                    PartnerTypeName = p.PartnerTypes.TypeName,
                    DirectorName = p.DirectorName,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address,
                    Rating = p.Rating
                }).ToList();
            }
        }

        public PartnerModel GetPartnerById(int id)
        {
            using (var db = DbContextFactory.Create())
            {
                var partner = db.Partners.Find(id);
                return new PartnerModel
                {
                    PartnerId = partner.PartnerID,
                    PartnerName = partner.PartnerName,
                    PartnerTypeId = partner.PartnerTypeID,
                    PartnerTypeName = partner.PartnerTypes.TypeName,
                    DirectorName = partner.DirectorName,
                    Phone = partner.Phone,
                    Email = partner.Email,
                    Address = partner.Address,
                    Rating = partner.Rating
                };
            }
        }

        public void UpdatePartnerModel(PartnerModel model)
        {
            using (var db = DbContextFactory.Create())
            {
                var partner = db.Partners.Find(model.PartnerId);
                if (partner != null)
                {
                    partner.PartnerName = model.PartnerName;
                    partner.PartnerTypeID = model.PartnerTypeId;
                    partner.DirectorName = model.DirectorName;
                    partner.Phone = model.Phone;
                    partner.Email = model.Email;
                    partner.Address = model.Address;
                    partner.Rating = model.Rating;
                    db.SaveChanges();
                }
            }
        }

        public decimal GetTotalSales(int partnerId)
        {
            using (var db = DbContextFactory.Create())
            {
                var result = db.SalesHistory
                    .Where(sh => sh.PartnerID == partnerId)
                    .Select(sh => (decimal?)sh.Quantity * sh.Products.Price)
                    .Sum();

                return result ?? 0m;
            }
        }


        public void AddPartner(PartnerModel partner)
        {
            if (partner == null) throw new ArgumentNullException(nameof(partner));
            if (partner.Rating < 0) throw new ArgumentException("Рейтинг не может быть отрицательным");

            using (var db = new PartnersDBEntities())
            {
                db.Partners.Add(new Partners
                {
                    PartnerName = partner.PartnerName,
                    PartnerTypeID = partner.PartnerTypeId,
                    DirectorName = partner.DirectorName,
                    Phone = partner.Phone,
                    Email = partner.Email,
                    Address = partner.Address,
                    Rating = partner.Rating
                });
                db.SaveChanges();
            }
        }

        public void UpdatePartner(PartnerModel partner)
        {
            using (var db = new PartnersDBEntities())
            {
                var entity = db.Partners.Find(partner.PartnerId);
                if (entity != null)
                {
                    entity.PartnerName = partner.PartnerName;
                    entity.PartnerTypeID = partner.PartnerTypeId;
                    entity.DirectorName = partner.DirectorName;
                    entity.Phone = partner.Phone;
                    entity.Email = partner.Email;
                    entity.Address = partner.Address;
                    entity.Rating = partner.Rating;
                    db.SaveChanges();
                }
            }
        }

        public void DeletePartner(int id)
        {
            using (var db = new PartnersDBEntities())
            {
                var partner = db.Partners.Find(id);
                if (partner != null)
                {
                    db.Partners.Remove(partner);
                    db.SaveChanges();
                }
            }
        }

        public int CalculateDiscount(decimal totalSales)
        {
            if (totalSales < 10000) return 0;
            if (totalSales < 50000) return 5;
            if (totalSales < 300000) return 10;
            return 15;
        }
    }
}
