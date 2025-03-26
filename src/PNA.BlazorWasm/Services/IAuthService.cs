using PNA.BlazorWasm.Models;

namespace PNA.BlazorWasm.Services;

public interface IAuthService
{
    Task<string> LoginAsync ( string email, string password );
    Task<UserDto> GetProfileAsync ();
    Task UpdateProfileAsync ( string firstName, string lastName );
    Task<IReadOnlyList<UserDto>> ListUsersAsync ();
    Task<UserDto> GetUserAsync ( Guid id );
    Task DeleteUserAsync ( Guid id );
    Task UpdateUserRoleAsync ( Guid id, string role );
    Task RegisterAsync ( string userName, string email, string password, string firstName, string lastName, string role ); 
    Task LogoutAsync ();
    }