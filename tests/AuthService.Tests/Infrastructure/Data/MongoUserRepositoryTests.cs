using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using PNA.AuthService.Infrastructure.Data;
using PNA.Core.Entities;
using PNA.Core.Enums;

namespace AuthService.Tests.Infrastructure.Data;

public class MongoUserRepositoryTests
    {
    private readonly Mock<IMongoCollection<User>> _mongoCollectionMock;
    private readonly MongoUserRepository _repository;

    public MongoUserRepositoryTests ()
        {
        _mongoCollectionMock = new Mock<IMongoCollection<User>>();

        // Mock configuration
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["MongoDB:ConnectionString"]).Returns("mongodb://localhost:27017");
        configurationMock.Setup(c => c["MongoDB:Database"]).Returns("TestDB");

        // Mock MongoClient and IMongoDatabase
        var clientMock = new Mock<MongoClient>();
        var databaseMock = new Mock<IMongoDatabase>();
        clientMock.Setup(c => c.GetDatabase("TestDB", null)).Returns(databaseMock.Object);
        databaseMock.Setup(d => d.GetCollection<User>("Users", null)).Returns(_mongoCollectionMock.Object);

        _repository = new MongoUserRepository(configurationMock.Object);
        }

    [Fact]
    public async Task AddAsync_ShouldInsertUser ()
        {
        // Arrange
        var user = new User(
            username: "testuser",
            passwordHash: "hash",
            email: "test@example.com",
            dateOfBirth: DateTime.Now,
            gender: Gender.Male,
            maritalStatus: MaritalStatus.SingleNeverMarried,
            address: null,
            jobStatus: JobStatus.FullTime,
            cellphone: null,
            imageUrl: null,
            lastLogin: null
        );

        // Act
        await _repository.AddAsync(user);

        // Assert
        _mongoCollectionMock.Verify(
            x => x.InsertOneAsync(
                It.Is<User>(u => u.Id == user.Id && u.Username == "testuser"),
                null,
                It.IsAny<CancellationToken>()),
            Times.Once());
        }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser ()
        {
        // Arrange
        var user = new User(
            username: "testuser",
            passwordHash: "hash",
            email: "test@example.com",
            dateOfBirth: DateTime.Now,
            gender: Gender.Male,
            maritalStatus: MaritalStatus.SingleNeverMarried,
            address: null,
            jobStatus: JobStatus.FullTime,
            cellphone: null,
            imageUrl: null,
            lastLogin: null
        );

        // Mock IAsyncCursor<User>
        var cursorMock = new Mock<IAsyncCursor<User>>();
        cursorMock.Setup(c => c.Current).Returns(new List<User> { user });
        cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(true)  // First call returns true to indicate data
                  .ReturnsAsync(false); // Subsequent calls return false to end iteration
        cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                  .Returns(true)
                  .Returns(false);

        // Mock FindAsync to return the cursor
        _mongoCollectionMock.Setup(m => m.FindAsync(
            It.Is<FilterDefinition<User>>(f => f != null), // Ensure filter is applied
            It.IsAny<FindOptions<User, User>>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(cursorMock.Object);

        // Act
        var result = await _repository.GetByIdAsync(user.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(user);
        _mongoCollectionMock.Verify(
            m => m.FindAsync(
                It.IsAny<FilterDefinition<User>>(),
                It.IsAny<FindOptions<User, User>>(),
                It.IsAny<CancellationToken>()),
            Times.Once());
        }
    }