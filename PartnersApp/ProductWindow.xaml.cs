using System.Windows;
using PartnersApp.Models;

namespace PartnersApp
{
    public partial class ProductWindow : Window
    {
        public Product Product { get; set; }

        public ProductWindow(Product product = null)
        {
            InitializeComponent();
            Product = product ?? new Product();
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Product.Name))
            {
                MessageBox.Show("Введите наименование продукта", "Ошибка");
                return;
            }

            if (Product.Price <= 0)
            {
                MessageBox.Show("Цена должна быть положительной", "Ошибка");
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