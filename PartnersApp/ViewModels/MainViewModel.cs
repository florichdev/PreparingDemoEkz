using System.Collections.Generic;
using System.ComponentModel;
using PartnersApp.Models;
using PartnersApp.Services;

namespace PartnersApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly PartnerService _partnerService = new PartnerService();
        private readonly ProductService _productService = new ProductService();
        private readonly SalesHistoryService _salesHistoryService = new SalesHistoryService();
        private bool _isAdmin;
        private decimal _totalSales;
        private int _discount;
        private List<PartnerModel> _partners;
        private List<ProductModel> _products;
        private PartnerModel _selectedPartner;
        private List<SaleHistoryModel> _partnerSales;

        public MainViewModel(bool isAdmin)
        {
            _isAdmin = isAdmin;
            LoadData();
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            private set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        public decimal TotalSales
        {
            get => _totalSales;
            private set
            {
                if (_totalSales != value)
                {
                    _totalSales = value;
                    OnPropertyChanged(nameof(TotalSales));
                    Discount = _partnerService.CalculateDiscount(value);
                }
            }
        }

        public int Discount
        {
            get => _discount;
            private set
            {
                if (_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged(nameof(Discount));
                }
            }
        }

        public List<PartnerModel> Partners
        {
            get => _partners;
            set
            {
                _partners = value;
                OnPropertyChanged(nameof(Partners));
            }
        }

        public List<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public PartnerModel SelectedPartner
        {
            get => _selectedPartner;
            set
            {
                _selectedPartner = value;
                OnPropertyChanged(nameof(SelectedPartner));
                if (value != null)
                {
                    LoadPartnerSales(value.PartnerId);
                }
            }
        }

        public List<SaleHistoryModel> PartnerSales
        {
            get => _partnerSales;
            set
            {
                _partnerSales = value;
                OnPropertyChanged(nameof(PartnerSales));
            }
        }

        public void LoadData()
        {
            Partners = new List<PartnerModel>(_partnerService.GetAllPartners());
            Products = new List<ProductModel>(_productService.GetAllProducts());
        }

        private void LoadPartnerSales(int partnerId)
        {
            PartnerSales = new List<SaleHistoryModel>(
                _salesHistoryService.GetSalesByPartner(partnerId));
            TotalSales = _selectedPartner != null ?
                _partnerService.GetTotalSales(_selectedPartner.PartnerId) : 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}