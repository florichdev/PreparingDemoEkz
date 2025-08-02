using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PartnersApp.Services;
using PartnersApp.ViewModels;
using System.Data.Metadata.Edm;

namespace PartnersApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var authWindow = new AuthWindow();
            authWindow.Show();
        }
    }
}
