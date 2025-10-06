using System;
using System.Windows;
using AdmFlorichWpf.Models;
using AdmFlorichWpf.Services;

namespace AdmFlorichWpf
{
    public partial class EditUsersWindow : Window
    {
        private UserModel _user;
        private Service _service = new Service();

        public EditUsersWindow(UserModel user = null)
        {
            InitializeComponent();

            _user = user ?? new UserModel
            {
                Family = new FamilyModel(),
                Role = false
            };

            DataContext = _user;

            Roletbx.Text = _user.Role.ToString();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_user.Username))
            {
                MessageBox.Show("Введите логин", "Ошибка");
                Usernametbx.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_user.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка");
                Passtbx.Focus();
                return false;
            }

            if (!bool.TryParse(Roletbx.Text, out bool role))
            {
                MessageBox.Show("Роль должна быть 'True' или 'False'", "Ошибка");
                Roletbx.Focus();
                return false;
            }

            if (!int.TryParse(Childstbx.Text, out int childs) || childs < 0)
            {
                MessageBox.Show("Количество детей должно быть целым неотрицательным числом", "Ошибка");
                Childstbx.Focus();
                return false;
            }

            return true;
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                _user.Role = bool.Parse(Roletbx.Text);

                _user.Family.Childs = int.Parse(Childstbx.Text);

                _service.SaveUser(_user);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.InnerException?.Message ?? ex.Message}", "Ошибка");
            }
        }

        private void CancelUser_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}