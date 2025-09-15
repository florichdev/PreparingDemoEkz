using PartnersApp.Models;
using PartnersApp.Services;
using System.ComponentModel;
using System.Windows;

namespace PartnersApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Service _dataService = new Service();
        private Partner _selectedPartner;

        public bool IsAdmin { get; }

        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            IsAdmin = isAdmin;
            DataContext = this;
            LoadData();
        }

        private void LoadData() => PartnersGrid.ItemsSource = _dataService.GetPartners();

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin && new PartnerWindow().ShowDialog() == true)
                LoadData();
        }

        private void EditPartner_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin && _selectedPartner != null && new PartnerWindow(_selectedPartner).ShowDialog() == true)
                LoadData();
        }

        private void DeletePartner_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin && _selectedPartner != null &&
                MessageBox.Show("Удалить партнера?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dataService.DeletePartner(_selectedPartner.Id);
                LoadData();
            }
        }

        private void ShowSales_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPartner != null)
            {
                new SalesWindow(_selectedPartner.Id, IsAdmin).ShowDialog();
                LoadData();
            }
        }

        private void PartnersGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
            => _selectedPartner = PartnersGrid.SelectedItem as Partner;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}