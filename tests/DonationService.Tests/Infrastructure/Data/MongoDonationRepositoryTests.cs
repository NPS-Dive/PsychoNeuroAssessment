using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.DonationService.Infrastructure.Data;

namespace DonationService.Tests.Infrastructure.Data;

public class MongoDonationRepositoryTests
    {
    private readonly Mock<IMongoCollection<Donation>> _mongoCollectionMock;
    private readonly MongoDonationRepository _repository;

    public MongoDonationRepositoryTests ()
        {
        _mongoCollectionMock = new Mock<IMongoCollection<Donation>>();
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["MongoDB:ConnectionString"]).Returns("mongodb://localhost:27017");
        configurationMock.Setup(c => c["MongoDB:Database"]).Returns("TestDB");
        var clientMock = new Mock<MongoClient>();
        var databaseMock = new Mock<IMongoDatabase>();
        clientMock.Setup(c => c.GetDatabase("TestDB", null)).Returns(databaseMock.Object);
        databaseMock.Setup(d => d.GetCollection<Donation>("Donations", null)).Returns(_mongoCollectionMock.Object);
        _repository = new MongoDonationRepository(configurationMock.Object);
        }

    [Fact]
    public async Task AddAsync_ShouldInsertDonation ()
        {
        var donation = new Donation(Guid.NewGuid(), 100m, BlockchainType.Ethereum, "txHash", DateTime.UtcNow);

        await _repository.AddAsync(donation);

        _mongoCollectionMock.Verify(x => x.InsertOneAsync(
            It.Is<Donation>(d => d.Id == donation.Id),
            null,
            It.IsAny<CancellationToken>()), Times.Once());
        }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDonation ()
        {
        var donation = new Donation(Guid.NewGuid(), 100m, BlockchainType.Ethereum, "txHash", DateTime.UtcNow);
        var cursorMock = new Mock<IAsyncCursor<Donation>>();
        cursorMock.Setup(c => c.Current).Returns(new List<Donation> { donation });
        cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
        cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
        _mongoCollectionMock.Setup(m => m.FindAsync(
            It.IsAny<FilterDefinition<Donation>>(),
            It.IsAny<FindOptions<Donation, Donation>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(cursorMock.Object);

        var result = await _repository.GetByIdAsync(donation.Id);

        result.Should().BeEquivalentTo(donation);
        }

    [Fact]
    public async Task UpdateAsync_ShouldReplaceDonation ()
        {
        var donation = new Donation(Guid.NewGuid(), 100m, BlockchainType.Ethereum, "txHash", DateTime.UtcNow);

        await _repository.UpdateAsync(donation);

        _mongoCollectionMock.Verify(x => x.ReplaceOneAsync(
            It.IsAny<FilterDefinition<Donation>>(),
            donation,
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());
        }
    }