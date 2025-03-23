using MediatR;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler ( IUserRepository userRepository, IPasswordHasher passwordHasher )
        {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        }

    public async Task<bool> Handle ( LoginCommand request, CancellationToken cancellationToken )
        {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null) return false;
        return _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        }
    }