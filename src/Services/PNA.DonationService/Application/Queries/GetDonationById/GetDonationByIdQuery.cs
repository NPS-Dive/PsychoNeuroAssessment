using MediatR;
using PNA.Core.Entities;

namespace PNA.DonationService.Application.Queries.GetDonationById;

public record GetDonationByIdQuery ( 
    Guid Id ) 
    : IRequest<Donation?>;