using System;
using System.Linq;
using System.Windows;
using PartnersApp.dbContext;
using PartnersApp.ViewModels;
using PartnersApp.Models;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            _viewModel = new MainViewModel(isAdmin);
            DataContext = _viewModel;
        }

        private void AddPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new PartnerEditWindow();
            if (window.ShowDialog() == true)
            {
                var service = new PartnerService();
                service.AddPartner(window.Partner);
                _viewModel.LoadData();
            }
        }

        private void EditPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPartner == null)
            {
                MessageBox.Show("Выберите партнера для редактирования", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var window = new PartnerEditWindow(_viewModel.SelectedPartner);
            if (window.ShowDialog() == true)
            {
                var service = new PartnerService();
                service.UpdatePartner(window.Partner);
                _viewModel.LoadData();
            }
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is ProductModel selectedProduct)
            {
                var window = new ProductEditWindow(selectedProduct);
                if (window.ShowDialog() == true)
                {
                    var service = new ProductService();
                    service.UpdateProduct(window.Product);
                    _viewModel.LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для редактирования", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePartnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPartner == null)
            {
                MessageBox.Show("Выберите партнера для удаления", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Удалить выбранного партнера?", "Подтверждение",
                              MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var partnerService = new PartnerService();
                partnerService.DeletePartner(_viewModel.SelectedPartner.PartnerId);
                _viewModel.LoadData();
            }
        }

        private void ShowSalesHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPartner != null)
            {
                var window = new SalesHistoryWindow(_viewModel.SelectedPartner.PartnerId, _viewModel.IsAdmin);
                window.ShowDialog();
                _viewModel.LoadData();
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductEditWindow();
            if (window.ShowDialog() == true)
            {
                var service = new ProductService();
                service.AddProduct(window.Product);
                _viewModel.LoadData();
            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is ProductModel selectedProduct)
            {
                if (MessageBox.Show("Удалить выбранный продукт?", "Подтверждение",
                                  MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var productService = new ProductService();
                    productService.DeleteProduct(selectedProduct.ProductId);
                    _viewModel.LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для удаления", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}