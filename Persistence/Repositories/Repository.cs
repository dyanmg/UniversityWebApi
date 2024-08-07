using Application.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class Repository<T>(SchoolContext _schoolContext) : IRepository<T> where T : BaseEntity
    {
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _schoolContext
                .Set<T>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _schoolContext
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetById(object id)
        {
            return await _schoolContext
                .Set<T>()
                .FindAsync(id);
        }

        public Task Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
