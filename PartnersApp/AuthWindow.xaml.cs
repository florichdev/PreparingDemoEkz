using PartnersApp.Services;
using System.Windows;

namespace PartnersApp
{
    public partial class AuthWindow : Window
    {
        private Service _dataService = new Service();

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (_dataService.Authenticate(LoginTextBox.Text, PasswordBox.Password, out bool isAdmin))
            {
                new MainWindow(isAdmin).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        }
    }
}