using MediatR;
using PNA.Core.Entities;

namespace PNA.AuthService.Application.Queries.GetUserById;

public record GetUserByIdQuery (
    Guid Id )
    : IRequest<User?>;