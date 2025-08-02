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
    public class ProductEditViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService = new ProductService();
        private ProductModel _product;

        public ProductEditViewModel(ProductModel product = null)
        {
            _product = product ?? new ProductModel();
        }

        public ProductModel Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}