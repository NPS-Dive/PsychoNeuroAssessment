using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Web3;
using PNA.DonationService.Infrastructure.Services;

namespace DonationService.Tests.Infrastructure.Services;

public class BlockchainServiceTests
{
    [Fact]
    public async Task ProcessDonationAsync_ShouldReturnTransactionHash ()
    {
        // Arrange
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["Ethereum:RpcUrl"]).Returns("https://testnet.infura.io/v3/KEY");
        configurationMock.Setup(c => c["Ethereum:PrivateKey"])
            .Returns("0x1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef");

        // Mock IWeb3
        var web3Mock = new Mock<IWeb3>();
        var etherTransferServiceMock = new Mock<IEtherTransferService>();
        etherTransferServiceMock
            .Setup(s => s.TransferEtherAsync(It.IsAny<string>(), It.IsAny<decimal>(), null, null, null))
            .ReturnsAsync("txHash");

        web3Mock.Setup(w => w.Eth.GetEtherTransferService()).Returns(etherTransferServiceMock.Object);

        // Instantiate the service with the mock
        var service = new BlockchainService(configurationMock.Object, web3Mock.Object);

        // Act
        var result = await service.ProcessDonationAsync(100m, Guid.NewGuid());

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Be("txHash"); // Optional: Verify exact value
    }
}