using MediatR;
using PNA.Core.Entities;

namespace PNA.Core.Queries;

public class ListUsersQuery : IRequest<IReadOnlyList<User>>
{
}