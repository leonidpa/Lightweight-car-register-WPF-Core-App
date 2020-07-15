using CarsRepositoryLibrary.Models;
using CarsRepositoryLibrary.Repositories;
using CarsRepositoryLibrary.Services;
using Lightweight_car_register_WPF_Core_App.ExtensionMethods;
using Lightweight_car_register_WPF_Core_App.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace Lightweight_car_register_WPF_Core_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataStore<Car> DataStore => RepositoryService.Get<CarsRepository>();

        CarsVM viewModel;

        protected delegate void Delegate();

        public MainWindow()
        {
            InitializeComponent();
            this.CenterWindowOnScreen();
            DataContext = viewModel = new CarsVM();

            var styles = new List<string> { "light", "dark" };
            styleComboBox.SelectionChanged += ThemeChange;
            styleComboBox.ItemsSource = styles;
            styleComboBox.SelectedItem = "dark";

            var brands = new List<string> { "All" };
            brands.AddRange(viewModel.Brands);
            brandComboBox.SelectionChanged += BrandChange;
            brandComboBox.ItemsSource = brands;
            brandComboBox.SelectedItem = "All";

            tbSearch.TextChanged += SearchChange;
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleComboBox.SelectedItem as string;
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void BrandChange(object sender, SelectionChangedEventArgs e)
        {
            if ((brandComboBox.SelectedItem as string)?.CompareTo("All") == 0)
            {
                viewModel.BrandFilterString = string.Empty;
            }
            else
            {
                viewModel.BrandFilterString = brandComboBox.SelectedItem as string;
            }
        }

        private void SearchChange(object sender, TextChangedEventArgs e)
        {
            viewModel.AllFilterString = tbSearch.Text;
        }

        protected async Task TryCatchUpdateVMErrMessageTask(Delegate _delegate)
        {
            try
            {
                _delegate();
                viewModel.LoadItemsCommand.Execute(null);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Cars Managment Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }


        private async void Edit_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (itemsList.SelectedItem == null) return;

            await TryCatchUpdateVMErrMessageTask(async () => 
            {
                var car = itemsList.SelectedItem as Car;

                var carWindow = new CarWindow(
                    new Car
                    {
                        Id = car.Id,
                        Model = car.Model,
                        Brand = car.Brand,
                        Owner = car.Owner
                    }
                    );

                if (carWindow.ShowDialog() == true)
                {
                    await DataStore.UpdateItemAsync(
                            new Car
                            {
                                Id = carWindow.Car.Id,
                                Model = carWindow.Car.Model,
                                Brand = carWindow.Car.Brand,
                                Owner = carWindow.Car.Owner
                            }
                        );
                }
            });
            
        }

        private async void Add_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            await TryCatchUpdateVMErrMessageTask(async () =>
            {
                var carWindow = new CarWindow(new Car());

                if (carWindow.ShowDialog() == true)
                {
                    if (string.IsNullOrEmpty(carWindow.Car.Model) || string.IsNullOrEmpty(carWindow.Car.Brand))
                    {
                        MessageBox.Show("Model and Brand are required!", "Cars Managment Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    await DataStore.AddItemAsync(
                            new Car
                            {
                                Model = carWindow.Car.Model,
                                Brand = carWindow.Car.Brand,
                                Owner = carWindow.Car.Owner
                            }
                        );
                }
            });
        }

        private async void Delete_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (itemsList.SelectedItem == null) return;

            await TryCatchUpdateVMErrMessageTask(async () =>
            {
                var messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var car = itemsList.SelectedItem as Car;
                    if (car != null)
                    {
                        await DataStore.DeleteItemAsync(car.Id.ToString());
                        viewModel.LoadItemsCommand.Execute(null);
                    }
                }
            });
        }
    }
}
