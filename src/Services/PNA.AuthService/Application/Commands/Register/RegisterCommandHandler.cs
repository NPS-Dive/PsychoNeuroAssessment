using MediatR;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Application.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler ( IUserRepository userRepository, IPasswordHasher passwordHasher )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle ( RegisterCommand request, CancellationToken cancellationToken )
    {
        var passwordHash = _passwordHasher.HashPassword(request.Password);
        var user = new User(request.Username, passwordHash, request.Email, DateTime.Now.AddYears(-30),
            Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);
        await _userRepository.AddAsync(user);
        return Unit.Value;
    }
}