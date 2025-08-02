using System.Linq;
using System.Windows;
using PartnersApp.dbContext;
using PartnersApp.Services;

namespace PartnersApp
{
    public partial class AuthWindow : Window
    {
        private readonly AuthService _authService = new AuthService();

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (_authService.Authenticate(login, password, out bool isAdmin))
            {
                MainWindow mainWindow = new MainWindow(isAdmin);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}