using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AdmFlorichWpf.dbContext;
using AdmFlorichWpf.Services;

namespace AdmFlorichWpf
{
    public partial class AuthWindow : Window
    {
        private Service _dataService = new Service();
        public AuthWindow()
        {
            InitializeComponent();
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            string Username = tbxlogin.Text;
            string Password = tbxpass.Password;

            if (_dataService.Authenticate(tbxlogin.Text, tbxpass.Password, out bool isRole))
            {
                MainWindow mainWindow = new MainWindow(isRole);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка, проверьте данные");
            }
        }
    }
}
