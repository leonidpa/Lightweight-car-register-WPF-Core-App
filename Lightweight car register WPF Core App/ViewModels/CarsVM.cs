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

        private string _filterString = string.Empty;

        public ICommand LoadItemsCommand { get; set; }

        public IEnumerable<Car> Items => _sourceItems.Where(e => Filter(e)).OrderBy(e => e.Brand);

        public CarsVM()
        {
            LoadItemsCommand = new DelegateCommand(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);

        }

        private bool Filter(object obj)
        {
            var car = obj as Car;
            return car.Brand.ToLower().Contains(_filterString.ToLower());
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

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged("FilterString");
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
