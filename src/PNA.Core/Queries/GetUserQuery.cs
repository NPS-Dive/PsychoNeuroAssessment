using MediatR;
using PNA.Core.Entities;

namespace PNA.Core.Queries;

public class GetUserQuery : IRequest<User>
{
    public Guid UserId { get; set; }

    public GetUserQuery ( Guid userId )
    {
        UserId = userId;
    }
}