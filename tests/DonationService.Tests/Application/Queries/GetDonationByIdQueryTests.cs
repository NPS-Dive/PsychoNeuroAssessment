using FluentAssertions;
using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;
using PNA.DonationService.Application.Queries.GetDonationById;

namespace DonationService.Tests.Application.Queries;

public class GetDonationByIdQueryTests
{
    [Fact]
    public async Task Handle_ShouldReturnDonation ()
    {
        var donationRepoMock = new Mock<IDonationRepository>();
        var donation = new Donation(Guid.NewGuid(), 100m, BlockchainType.Ethereum, "txHash", DateTime.UtcNow);
        donationRepoMock.Setup(x => x.GetByIdAsync(donation.Id)).ReturnsAsync(donation);
        var handler = new GetDonationByIdQueryHandler(donationRepoMock.Object);
        var query = new GetDonationByIdQuery(donation.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().BeEquivalentTo(donation);
    }
}