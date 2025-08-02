using System.Linq;
using System.Windows;
using System.Windows.Input;
using PartnersApp.dbContext;
using PartnersApp.Models;

namespace PartnersApp
{
    public partial class PartnerEditWindow : Window
    {
        public PartnerModel Partner { get; set; }

        public PartnerEditWindow(PartnerModel partner = null)
        {
            InitializeComponent();
            Partner = partner ?? new PartnerModel();
            DataContext = this;
            LoadPartnerTypes();
        }

        private void LoadPartnerTypes()
        {
            using (var db = new PartnersDBEntities())
            {
                PartnerTypeComboBox.ItemsSource = db.PartnerTypes.ToList();
                if (Partner.PartnerTypeId > 0)
                {
                    PartnerTypeComboBox.SelectedValue = Partner.PartnerTypeId;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Partner.PartnerName))
            {
                MessageBox.Show("Введите наименование партнера", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(RatingTextBox.Text, out int rating) || rating < 0)
            {
                MessageBox.Show("Введите корректный рейтинг (целое число >= 0)", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Partner.Rating = rating;

            if (PartnerTypeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип партнера", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Partner.PartnerTypeId = (int)PartnerTypeComboBox.SelectedValue;

            DialogResult = true;
            Close();
        }

        private void RatingTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}