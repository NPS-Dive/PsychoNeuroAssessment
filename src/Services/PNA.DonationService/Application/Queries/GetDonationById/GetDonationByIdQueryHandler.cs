using MediatR;
using PNA.Core.Entities;
using PNA.Core.Interfaces;

namespace PNA.DonationService.Application.Queries.GetDonationById;

public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, Donation?>
{
    private readonly IDonationRepository _donationRepository;

    public GetDonationByIdQueryHandler ( IDonationRepository donationRepository )
    {
        _donationRepository = donationRepository;
    }

    public async Task<Donation?> Handle ( GetDonationByIdQuery request, CancellationToken cancellationToken )
    {
        return await _donationRepository.GetByIdAsync(request.Id);
    }
}