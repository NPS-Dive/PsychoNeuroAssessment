using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.AuthService.Application.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler ( IUserRepository userRepository )
        {
        _userRepository = userRepository;
        }

    public async Task<List<User>> Handle ( GetAllUsersQuery request, CancellationToken cancellationToken )
        {
        return await _userRepository.GetAllAsync();
        }
    }