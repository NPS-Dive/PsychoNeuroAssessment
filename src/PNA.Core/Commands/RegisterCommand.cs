using MediatR;

namespace PNA.Core.Commands;

public class RegisterCommand : IRequest<Guid>
{
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Role { get; }

    private static readonly string[] AllowedRoles = { "User", "Admin" };

    public RegisterCommand ( string userName, string email, string password, string firstName, string lastName, string role )
    {
        if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Username cannot be empty", nameof(userName));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty", nameof(email));
        if (!AllowedRoles.Contains(role)) throw new ArgumentException($"Role must be one of: {string.Join(", ", AllowedRoles)}", nameof(role));

        UserName = userName;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }
}