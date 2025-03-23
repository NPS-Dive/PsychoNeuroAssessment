using FluentAssertions;
using MediatR;
using PNA.Core.Enums;
using PNA.TestService.Application.Commands.AssignTest;

namespace TestService.Tests.Application.Commands;

public class AssignTestCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnUnit ()
    {
        var handler = new AssignTestCommandHandler();
        var command = new AssignTestCommand(Guid.NewGuid(), TestType.Cognitive);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().Be(Unit.Value);
    }
}