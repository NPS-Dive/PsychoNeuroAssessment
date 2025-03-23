using MediatR;

namespace PNA.Core.Commands;

public abstract record BaseCommand<TResponse> : IRequest<TResponse>
{
    public Guid CommandId { get; set; } = Guid.NewGuid();
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
}