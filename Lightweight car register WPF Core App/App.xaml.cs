using CarsRepositoryLibrary.Repositories;
using CarsRepositoryLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lightweight_car_register_WPF_Core_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void Application_Start(object sender, StartupEventArgs args)
        {
            RepositoryService.Register<CarsRepository>(@"Data Source=.\cars.db");
        }
    }
}
