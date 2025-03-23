using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.Core.Interfaces;
using PNA.DonationService.Application.Commands.ProcessDonation;

namespace DonationService.Tests.Application.Commands;

public class ProcessDonationCommandTests
{
    [Fact]
    public async Task Handle_ShouldAddDonation ()
    {
        var donationRepoMock = new Mock<IDonationRepository>();
        var blockchainServiceMock = new Mock<IBlockchainService>();
        blockchainServiceMock.Setup(x => x.ProcessDonationAsync(100m, It.IsAny<Guid>())).ReturnsAsync("txHash");
        var handler = new ProcessDonationCommandHandler(donationRepoMock.Object, blockchainServiceMock.Object);
        var command = new ProcessDonationCommand(Guid.NewGuid(), 100m, BlockchainType.Ethereum);

        await handler.Handle(command, CancellationToken.None);

        donationRepoMock.Verify(x => x.AddAsync(It.Is<Donation>(d =>
            d.UserId == command.UserId &&
            d.Amount == 100m &&
            d.TransactionHash == "txHash")), Times.Once);
    }
}