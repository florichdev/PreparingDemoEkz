using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdmFlorichWpf.dbContext;
using AdmFlorichWpf.Services;
using AdmFlorichWpf.Models;

namespace AdmFlorichWpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public bool IsRole { get; }
        private Service _service = new Service();
        private UserModel _selectedUser;

        public MainWindow(bool isRole)
        {
            InitializeComponent();
            IsRole = isRole;
            DataContext = this;
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                var users = _service.GetUsersWithFamily();
                MainGrid.ItemsSource = users;
                PasswordColumn.Visibility = IsRole ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка");
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (IsRole)
            {
                var window = new EditUsersWindow();
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (IsRole && _selectedUser != null)
            {
                var window = new EditUsersWindow(_selectedUser);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для редактирования", "Информация");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (IsRole && _selectedUser != null)
            {
                var result = MessageBox.Show($"Удалить пользователя {_selectedUser.Username}?", "Подтверждение удаления",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _service.DeleteUser(_selectedUser.UserId);
                        LoadData();
                        MessageBox.Show("Пользователь удален", "Успех");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления", "Информация");
            }
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUser = MainGrid.SelectedItem as UserModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            new ExportService().ExportToExcel(MainGrid);
        }
    }
}