using MediatR;
using PNA.Core.Commands;
using PNA.Core.Enums;

namespace PNA.TestService.Application.Commands.AssignTest;

public record AssignTestCommand (
    Guid UserId,
    TestType TestType )
    : BaseCommand<Unit>;