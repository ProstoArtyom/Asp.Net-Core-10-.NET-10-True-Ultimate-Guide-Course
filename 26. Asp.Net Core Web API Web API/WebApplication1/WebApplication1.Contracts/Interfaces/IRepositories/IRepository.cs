using WebApplication1.Entities;

namespace WebApplication1.Contracts.Interfaces.IRepositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> AddAsync(T item);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> items); 
        Task UpdateAsync(T item);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}