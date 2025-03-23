using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Application.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler ( IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle ( GetUserByIdQuery request, CancellationToken cancellationToken )
    {
        return await _userRepository.GetByIdAsync(request.Id);
    }
}