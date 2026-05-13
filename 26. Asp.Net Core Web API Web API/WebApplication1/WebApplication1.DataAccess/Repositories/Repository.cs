using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts.Interfaces.IRepositories;
using WebApplication1.Entities;

namespace WebApplication1.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<T> _db;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _db = _dbContext.Set<T>();
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            var item = await _db.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (item == null)
                throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found");

            return item;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await _db.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            await _db.AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();

            return items;
        }

        public virtual async Task UpdateAsync(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var existing = await _db.FindAsync(item.Id);
            if (existing == null)
                throw new KeyNotFoundException($"{typeof(T).Name} with id {item.Id} not found");

            _dbContext.Entry(existing).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var itemToDelete = await _db.FindAsync(id);
            if (itemToDelete == null)
                throw new KeyNotFoundException($"{typeof(T).Name} with id {id} not found");

            _db.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _db.AnyAsync(temp => temp.Id == id);
        }
    }
}
