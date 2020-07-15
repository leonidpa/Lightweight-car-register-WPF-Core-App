using CarsRepositoryLibrary.Models;
using CarsRepositoryLibrary.Repositories;
using CarsRepositoryLibrary.Services;
using Lightweight_car_register_WPF_Core_App.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Lightweight_car_register_WPF_Core_App.ViewModels
{
    public class CarsVM : INotifyPropertyChanged
    {
        private IDataStore<Car> DataStore => RepositoryService.Get<CarsRepository>();

        private IEnumerable<Car> _sourceItems { get; set; }

        private string _allfilterString = string.Empty;

        private string _brandfilterString = string.Empty;

        public ICommand LoadItemsCommand { get; set; }

        public IEnumerable<Car> Items => _sourceItems.Where(e => Filter(e)).OrderBy(e => e.Brand);

        public IEnumerable<string> Brands => _sourceItems.GroupBy(e => e.Brand.ToLower()).Select(e => e.Key).ToList();

        public CarsVM()
        {
            LoadItemsCommand = new DelegateCommand(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);

        }

        private bool Filter(object obj)
        {
            var car = obj as Car;
            var result = false;
            Func<bool> searchAll = delegate ()
            {
                return car.Brand.ToLower().Contains(_allfilterString.ToLower())
                ||
                car.Model.ToLower().Contains(_allfilterString.ToLower())
                ||
                car.Owner.ToLower().Contains(_allfilterString.ToLower());
            };
            if (string.IsNullOrEmpty(_brandfilterString))
            {
                result = searchAll();
            }
            else
            {
                result = 
                searchAll()
                &&
                car.Brand.ToLower().Contains(_brandfilterString.ToLower());
            }
            return result;
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                (DataStore as CarsRepository)?.Seed();
                _sourceItems = await DataStore.GetItemsAsync();
                OnPropertyChanged("Items");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public string AllFilterString
        {
            get { return _allfilterString; }
            set
            {
                _allfilterString = value;
                OnPropertyChanged("AllFilterString");
                OnPropertyChanged("Items");
            }
        }

        public string BrandFilterString
        {
            get { return _brandfilterString; }
            set
            {
                _brandfilterString = value;
                OnPropertyChanged("BrandFilterString");
                OnPropertyChanged("Items");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
