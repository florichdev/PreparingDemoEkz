using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PartnersApp.Models;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class AddSaleWindow : Window
    {
        private readonly int _partnerId;
        private readonly ProductService _productService = new ProductService();

        public ProductModel SelectedProduct => ProductComboBox.SelectedItem as ProductModel;
        public int Quantity { get; private set; }

        public AddSaleWindow(int partnerId)
        {
            InitializeComponent();
            _partnerId = partnerId;
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductComboBox.ItemsSource = _productService.GetAllProducts();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Выберите продукт", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Quantity = quantity;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
