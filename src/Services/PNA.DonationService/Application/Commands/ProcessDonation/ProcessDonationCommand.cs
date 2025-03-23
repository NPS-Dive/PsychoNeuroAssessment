using MediatR;
using PNA.Core.Commands;
using PNA.Core.Enums;

namespace PNA.DonationService.Application.Commands.ProcessDonation;

public record ProcessDonationCommand (
    Guid UserId, 
    decimal Amount, 
    BlockchainType BlockchainType ) 
    : BaseCommand<Unit>;