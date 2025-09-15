using System.Windows;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class SalesWindow : Window
    {
        private readonly Service _dataService = new Service();
        private readonly int _partnerId;

        public SalesWindow(int partnerId, bool isAdmin)
        {
            InitializeComponent();
            _partnerId = partnerId;
            BtnAddSale.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            SalesDataGrid.ItemsSource = _dataService.GetSales(_partnerId);
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddSaleWindow(_partnerId);
            if (dialog.ShowDialog() == true)
            {
                _dataService.AddSale(_partnerId, dialog.ProductId, dialog.Quantity);
                LoadSalesHistory();
                MessageBox.Show("Продажа успешно добавлена", "Успех");
            }
        }
    }
}