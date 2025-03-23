using MediatR;
using PNA.Core.Commands;
using PNA.Core.Enums;

namespace PNA.TestService.Application.Commands.SubmitTestResponses;

public record SubmitTestResponsesCommand (
    Guid UserId,
    TestType TestType,
    Dictionary<string, int> Responses )
    : BaseCommand<Unit>;