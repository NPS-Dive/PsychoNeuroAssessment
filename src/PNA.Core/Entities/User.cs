using Microsoft.AspNetCore.Identity;

namespace PNA.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public User ( string userName, string email, string firstName, string lastName ) : base(userName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    private User () { }

    public void Update ( string firstName, string lastName )
    {
        FirstName = firstName;
        LastName = lastName;
        UpdatedAt = DateTime.UtcNow;
    }
}