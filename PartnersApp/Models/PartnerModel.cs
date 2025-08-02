using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnersApp.Models
{
    public class PartnerModel
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public int PartnerTypeId { get; set; }
        public string PartnerTypeName { get; set; }
        public string DirectorName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Рейтинг не может быть отрицательным");
                }
                _rating = value;
            }
        }
    }

}
