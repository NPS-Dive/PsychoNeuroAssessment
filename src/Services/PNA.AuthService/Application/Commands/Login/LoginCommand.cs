using PNA.Core.Commands;

namespace PNA.AuthService.Application.Commands.Login;

public record LoginCommand ( 
    string Username, 
    string Password)
    : BaseCommand<bool>;