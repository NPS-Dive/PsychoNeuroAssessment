using MediatR;

namespace PNA.Core.Commands;

public class RegisterCommand : IRequest<Guid>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }

    public RegisterCommand ( string userName, string email, string password, string firstName, string lastName, string role )
    {
        UserName = userName;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }
}