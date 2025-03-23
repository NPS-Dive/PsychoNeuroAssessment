using MediatR;
using PNA.Core.Commands;

namespace PNA.AuthService.Application.Commands.UpdateUser;

public record UpdateUserCommand (
    Guid Id,
    string? Username,
    string? Password,
    string? Email )
    : BaseCommand<Unit>;