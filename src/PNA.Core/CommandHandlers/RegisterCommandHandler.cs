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
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle ( RegisterCommand request, CancellationToken cancellationToken )
    {
        var user = new User(request.UserName, request.Email, request.FirstName, request.LastName)
        {
            EmailConfirmed = true // Add confirmation flow later if needed
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new Exception($"Registration failed: {result.Errors.First().Description}");

        // Assign role in SQL Server (Identity)
        await _userManager.AddToRoleAsync(user, request.Role);

        // Store user in MongoDB (roles managed by Identity, but user data mirrored)
        await _userRepository.AddAsync(user);

        return user.Id;
    }
}