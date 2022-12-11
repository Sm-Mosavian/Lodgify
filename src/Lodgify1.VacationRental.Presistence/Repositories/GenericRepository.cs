using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Microsoft.EntityFrameworkCore;

namespace Lodgify1.VacationRental.Presistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VacationRentalDbContext _dbContext;
        public GenericRepository(VacationRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
           _dbContext.Entry(entity).State=EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
