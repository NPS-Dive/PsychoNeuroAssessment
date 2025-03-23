using MediatR;

namespace PNA.TestService.Application.Commands.AssignTest;

public class AssignTestCommandHandler : IRequestHandler<AssignTestCommand, Unit>
{
    public Task<Unit> Handle ( AssignTestCommand request, CancellationToken cancellationToken )
    {
        // Logic to assign a test (e.g., queue via MassTransit)
        return Task.FromResult(Unit.Value);
    }
}