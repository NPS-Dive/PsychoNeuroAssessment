using MediatR;
using PNA.Core.Entities;

namespace PNA.AuthService.Application.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<List<User>>;