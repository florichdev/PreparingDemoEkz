using System.Windows;
using PartnersApp.Models;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class AddSaleWindow : Window
    {
        private readonly Service _dataService = new Service();

        public int ProductId { get; private set; }
        public int Quantity { get; private set; }

        public AddSaleWindow(int partnerId)
        {
            InitializeComponent();
            ProductComboBox.ItemsSource = _dataService.GetProducts();
            ProductComboBox.DisplayMemberPath = "Name";
            ProductComboBox.SelectedValuePath = "Id";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedValue == null || !int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Проверьте данные", "Ошибка");
                return;
            }

            ProductId = (int)ProductComboBox.SelectedValue;
            Quantity = quantity;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}