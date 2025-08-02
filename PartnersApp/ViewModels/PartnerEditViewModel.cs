using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartnersApp.Models;
using PartnersApp.Services;
using System.ComponentModel;

namespace PartnersApp.ViewModels
{
    public class PartnerEditViewModel : INotifyPropertyChanged
    {
        private readonly PartnerService _partnerService = new PartnerService();
        private PartnerModel _partner;

        public PartnerEditViewModel(PartnerModel partner = null)
        {
            _partner = partner ?? new PartnerModel();
            PartnerTypes = GetPartnerTypes();
        }

        public PartnerModel Partner
        {
            get => _partner;
            set
            {
                _partner = value;
                OnPropertyChanged(nameof(Partner));
            }
        }

        public List<PartnerTypeModel> PartnerTypes { get; }

        private List<PartnerTypeModel> GetPartnerTypes()
        {
            using (var db = DbContextFactory.Create())
            {
                return db.PartnerTypes.Select(pt => new PartnerTypeModel
                {
                    PartnerTypeId = pt.PartnerTypeID,
                    TypeName = pt.TypeName
                }).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}