﻿using Microsoft.EntityFrameworkCore;
using Route.C41.BLL.Interface;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;
        
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
            => _dbContext.Add(entity);

        public void Update(T entity)
            => _dbContext.Update(entity);
        public void Delete(T entity)
            =>_dbContext.Remove(entity);
         
        public async Task<T> GetAsync(int id)
            =>await _dbContext.FindAsync<T>(id);
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
                return  (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            else
                return await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        }


    }
}
