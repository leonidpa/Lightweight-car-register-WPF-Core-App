﻿using CarsRepositoryLibrary.Models;
using CarsRepositoryLibrary.Repositories;
using CarsRepositoryLibrary.Services;
using Lightweight_car_register_WPF_Core_App.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lightweight_car_register_WPF_Core_App.ViewModels
{
    public class CarsVM : INotifyPropertyChanged
    {
        private IDataStore<Car> DataStore => RepositoryService.Get<CarsRepository>();

        public ObservableCollection<Car> Items { get; set; }

        public ICommand LoadItemsCommand { get; set; }

        public CarsVM()
        {
            Items = new ObservableCollection<Car>();
            LoadItemsCommand = new DelegateCommand(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);
        } 

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
                (DataStore as CarsRepository)?.Seed();
                var items = await DataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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
