using PNA.Core.Entities;

namespace PNA.Core.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUsernameAsync ( string username );
    Task<List<User>> GetAllAsync ();
}