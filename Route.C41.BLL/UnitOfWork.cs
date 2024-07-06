using Route.C41.BLL.Interface;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        //private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
            // _repositories = new Hashtable();
        }

        public IEmployeeRepository EmployeeRepository
        {
            get ;
            set ;
        }
        public IDepartmentRepository DepartmentRepository
        {
            get ;
            set ;
        }

       


        ///public IGenericRepository<T> repository<T>() where T : ModelBase
        ///{
        ///    var key = typeof(T).Name;
        ///    if (!_repositories.ContainsKey(key))
        ///    {
        ///        if (key == nameof(Employee))
        ///        {
        ///            var repository = new EmployeeRepository(_dbContext);
        ///            _repositories.Add(key, repository);
        ///        }
        ///        else if (key == nameof(Department))
        ///        {
        ///            var repository = new DepartmentRepository(_dbContext);
        ///            _repositories.Add(key, repository);
        ///        }
        ///
        ///    }
        ///
        ///    return _repositories[key] as IGenericRepository<T>;
        ///}

        public async Task<int> Complete()
        {
           return await _dbContext.SaveChangesAsync();
        }

       
        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
