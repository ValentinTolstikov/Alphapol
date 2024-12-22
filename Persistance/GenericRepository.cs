using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class GenericRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly CUsersUserSourceReposPraktikaPersistanceDbMdfContext _context;

        public GenericRepository(CUsersUserSourceReposPraktikaPersistanceDbMdfContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(new object[] { id });
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                   .Where(predicate)
                   .ToListAsync();
        }

        public Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync();
        }

        public async Task EditAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void ResetChanges() => _context.ChangeTracker.Entries()
    .Where(e => e.Entity != null).ToList()
    .ForEach(e => e.State = EntityState.Detached);
    }
}
