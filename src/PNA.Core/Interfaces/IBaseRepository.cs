using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task AddAsync ( T entity );
    Task<T?> GetByIdAsync ( Guid id );
    Task UpdateAsync ( T entity );
}