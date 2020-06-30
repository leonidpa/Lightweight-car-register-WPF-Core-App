using CarsRepositoryLibrary.Contexts;
using CarsRepositoryLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRepositoryLibrary.Repositories
{
    public class CarsRepository : IDataStore<Car>, ICreatable
    {
        public async Task<bool> EnsureCreated()
        {
            using var dbCtx = new CarsDbContext();
            
            return await Task.FromResult(dbCtx.Database.EnsureCreated());
        }

        public async Task<bool> AddItemAsync(Car model)
        {
            using var dbCtx = new CarsDbContext();
            dbCtx.Cars.Add(model);
            dbCtx.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Car>> GetItemsAsync(bool forceRefresh = false)
        {
            using var dbCtx = new CarsDbContext();
            dbCtx.Cars.Load();
            return await Task.FromResult(dbCtx.Cars.Local.ToBindingList());
        }

        public async Task<Car> GetItemAsync(string id)
        {
            if (!Int32.TryParse(id, out int Id)) return null;
            using var dbCtx = new CarsDbContext();
            return await Task.FromResult(dbCtx.Cars.SingleOrDefault(e => e.Id == Id));
        }

        public async Task<bool> UpdateItemAsync(Car model)
        {
            if (model == null) return false;
            using var dbCtx = new CarsDbContext();
            var dbModel = dbCtx.Cars.SingleOrDefault(e => e.Id == model.Id);
            if (dbModel != null)
            {
                dbModel.Brand = model.Brand;
                dbModel.Model = model.Model;
                dbModel.Owner = model.Owner;
                dbCtx.SaveChanges();
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (!Int32.TryParse(id, out int Id)) return false;
            using var dbCtx = new CarsDbContext();
            _ = dbCtx.Cars.Remove(dbCtx.Cars.SingleOrDefault(e => e.Id == Id));
            dbCtx.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> Seed()
        {
            if ((await GetItemsAsync()).Count() < 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    await AddItemAsync(new Car
                    {
                        Brand = "seedBrand#" + i,
                        Model = "seedModel#" + i,
                        Owner = "seedOwner#" + i
                    });
                }
            }

            return await Task.FromResult(true);
        }
    }
}
