using System.Linq;
using System.Windows;
using PartnersApp.dbContext;
using PartnersApp.Models;
using PartnersApp.Services;
using System;
using System.Windows.Controls;

namespace PartnersApp
{
    public partial class SalesHistoryWindow : Window
    {
        private readonly int _partnerId;
        private readonly bool _isAdmin;
        private readonly SalesHistoryService _salesHistoryService = new SalesHistoryService();
        private readonly SalesService _salesService = new SalesService();

        public SalesHistoryWindow(int partnerId, bool isAdmin)
        {
            InitializeComponent();
            _partnerId = partnerId;
            _isAdmin = isAdmin;
            BtnAddSale.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            SalesDataGrid.ItemsSource = _salesHistoryService.GetSalesByPartner(_partnerId);
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddSaleWindow(_partnerId);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _salesService.AddSale(
                        _partnerId,
                        dialog.SelectedProduct.ProductId,
                        dialog.Quantity,
                        DateTime.Now);

                    LoadSalesHistory();
                    MessageBox.Show("Продажа успешно добавлена", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении продажи: {ex.Message}",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}