using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.DonationService.Application.Commands.ProcessDonation;

public class ProcessDonationCommandHandler : IRequestHandler<ProcessDonationCommand, Unit>
{
    private readonly IDonationRepository _donationRepository;
    private readonly IBlockchainService _blockchainService;

    public ProcessDonationCommandHandler ( IDonationRepository donationRepository, IBlockchainService blockchainService )
    {
        _donationRepository = donationRepository;
        _blockchainService = blockchainService;
    }

    public async Task<Unit> Handle ( ProcessDonationCommand request, CancellationToken cancellationToken )
    {
        var txHash = await _blockchainService.ProcessDonationAsync(request.Amount, request.UserId);
        var donation = new Donation(request.UserId, request.Amount, request.BlockchainType, txHash, DateTime.UtcNow);
        await _donationRepository.AddAsync(donation);
        return Unit.Value;
    }
}