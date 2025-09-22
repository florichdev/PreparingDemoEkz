using PartnersApp.Models;
using PartnersApp.Services;
using System.ComponentModel;
using System.Windows;

namespace PartnersApp
{
    public partial class MainWindow : Window
    {
        public bool IsAdmin { get; }

        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            IsAdmin = isAdmin;
            DataContext = this;
        }
    }
}