using System.Windows;
using PartnersApp.Models;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class ProductEditWindow : Window
    {
        public ProductModel Product { get; set; }

        public ProductEditWindow(ProductModel product = null)
        {
            InitializeComponent();
            Product = product ?? new ProductModel();
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Product.ProductName))
            {
                MessageBox.Show("Введите наименование продукта", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Product.Price <= 0)
            {
                MessageBox.Show("Цена должна быть положительной", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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