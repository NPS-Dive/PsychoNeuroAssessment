using MediatR;
using PNA.Core.Commands;

namespace PNA.AuthService.Application.Commands.Register;

public record RegisterCommand (
    string Username,
    string Password,
    string Email )
    : BaseCommand<Unit>;