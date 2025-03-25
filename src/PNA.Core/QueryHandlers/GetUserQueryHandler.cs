using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;
using PNA.Core.Queries;

namespace PNA.Core.QueryHandlers;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler ( IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle ( GetUserQuery request, CancellationToken cancellationToken )
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null) throw new Exception("User not found");
        return user;
    }
}