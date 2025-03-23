using MediatR;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Application.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserCommandHandler ( IUserRepository userRepository, IPasswordHasher passwordHasher )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle ( UpdateUserCommand request, CancellationToken cancellationToken )
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null) throw new Exception("User not found");

        user.Username = request.Username ?? user.Username;
        if (request.Password != null) user.PasswordHash = _passwordHasher.HashPassword(request.Password);
        user.Email = request.Email ?? user.Email;
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}