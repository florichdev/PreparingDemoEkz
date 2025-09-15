using System.Linq;
using System.Windows;
using PartnersApp.dbContext;
using PartnersApp.Models;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class PartnerWindow : Window
    {
        public Partner Partner { get; set; }

        public PartnerWindow(Partner partner = null)
        {
            InitializeComponent();
            Partner = partner ?? new Partner();
            DataContext = this;
            LoadPartnerTypes();
        }

        private void LoadPartnerTypes()
        {
            using (var db = new PartnersDBEntities())
            {
                PartnerTypeComboBox.ItemsSource = db.PartnerTypes.ToList();
                if (Partner.TypeId > 0) PartnerTypeComboBox.SelectedValue = Partner.TypeId;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Partner.Name) ||
                !int.TryParse(RatingTextBox.Text, out int rating) || rating < 0 ||
                PartnerTypeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Проверьте введенные данные", "Ошибка");
                return;
            }

            Partner.Rating = rating;
            Partner.TypeId = (int)PartnerTypeComboBox.SelectedValue;

            using (var service = new Service())
                service.SavePartner(Partner);

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}