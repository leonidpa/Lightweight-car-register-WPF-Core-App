using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarsRepositoryLibrary.Repositories
{
    public interface ICreatable
    {
        Task<bool> EnsureCreated();
    }
}
