using MediatR;
using Microsoft.AspNetCore.Identity;
using PNA.Core.Commands;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.Core.CommandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler ( UserManager<User> userManager, IUserRepository userRepository )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Guid> Handle ( RegisterCommand request, CancellationToken cancellationToken )
    {
        var user = CreateUser(request);
        await RegisterUserAsync(user, request.Password);
        await AssignRoleAsync(user, request.Role);
        await SaveToRepositoryAsync(user);

        return user.Id;
    }

    private User CreateUser ( RegisterCommand request ) =>
        new User(request.UserName, request.Email, request.FirstName, request.LastName);

    private async Task RegisterUserAsync ( User user, string password )
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new InvalidOperationException($"Registration failed: {result.Errors.First().Description}");
    }

    private async Task AssignRoleAsync ( User user, string role )
    {
        await _userManager.AddToRoleAsync(user, role);
        user.Roles = (await _userManager.GetRolesAsync(user)).ToList();
    }

    private async Task SaveToRepositoryAsync ( User user )
    {
        await _userRepository.AddAsync(user);
    }
}