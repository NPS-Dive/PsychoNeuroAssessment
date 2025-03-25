using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> AddAsync ( TEntity entity );
    Task<TEntity?> GetByIdAsync ( Guid id );
    Task UpdateAsync ( TEntity entity );
    Task DeleteAsync ( Guid id );
}