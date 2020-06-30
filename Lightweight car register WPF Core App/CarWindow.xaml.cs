using CarsRepositoryLibrary.Models;
using Lightweight_car_register_WPF_Core_App.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lightweight_car_register_WPF_Core_App
{
    /// <summary>
    /// Interaction logic for CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {
        public Car Car { get; private set; }

        public CarWindow(Car car)
        {
            InitializeComponent();
            this.CenterWindowOnScreen();
            Car = car;
            DataContext = Car;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
