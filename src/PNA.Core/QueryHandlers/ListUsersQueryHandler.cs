using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;
using PNA.Core.Queries;

namespace PNA.Core.QueryHandlers;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IReadOnlyList<User>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersQueryHandler ( IUserRepository userRepository )
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<IReadOnlyList<User>> Handle ( ListUsersQuery request, CancellationToken cancellationToken )
    {
        var users = await FetchUsersAsync();
        return users.AsReadOnly();
    }

    private async Task<List<User>> FetchUsersAsync ()
    {
        var users = new List<User>();
        // MongoDB doesn't have a direct "list all" in IUserRepository, so simulate via FindByEmailAsync
        // In a real app, extend IUserRepository with GetAllAsync if needed
        var admin = await _userRepository.FindByEmailAsync("admin@example.com"); // Example seed
        if (admin != null) users.Add(admin);
        return users; // Placeholder; extend repository for full list
    }
}