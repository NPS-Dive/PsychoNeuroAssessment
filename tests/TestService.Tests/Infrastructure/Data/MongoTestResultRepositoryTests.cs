using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.TestService.Infrastructure.Data;

namespace TestService.Tests.Infrastructure.Data;

public class MongoTestResultRepositoryTests
    {
    private readonly Mock<IMongoCollection<TestResult>> _mongoCollectionMock;
    private readonly MongoTestResultRepository _repository;

    public MongoTestResultRepositoryTests ()
        {
        _mongoCollectionMock = new Mock<IMongoCollection<TestResult>>();
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["MongoDB:ConnectionString"]).Returns("mongodb://localhost:27017");
        configurationMock.Setup(c => c["MongoDB:Database"]).Returns("TestDB");
        var clientMock = new Mock<MongoClient>();
        var databaseMock = new Mock<IMongoDatabase>();
        clientMock.Setup(c => c.GetDatabase("TestDB", null)).Returns(databaseMock.Object);
        databaseMock.Setup(d => d.GetCollection<TestResult>("TestResults", null)).Returns(_mongoCollectionMock.Object);
        _repository = new MongoTestResultRepository(configurationMock.Object);
        }

    [Fact]
    public async Task AddAsync_ShouldInsertTestResult ()
        {
        var testResult = new TestResult(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 75.0, DateTime.UtcNow);

        await _repository.AddAsync(testResult);

        _mongoCollectionMock.Verify(x => x.InsertOneAsync(
            It.Is<TestResult>(tr => tr.Id == testResult.Id),
            null,
            It.IsAny<CancellationToken>()), Times.Once());
        }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTestResult ()
        {
        var testResult = new TestResult(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 75.0, DateTime.UtcNow);
        var cursorMock = new Mock<IAsyncCursor<TestResult>>();
        cursorMock.Setup(c => c.Current).Returns(new List<TestResult> { testResult });
        cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
        cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
        _mongoCollectionMock.Setup(m => m.FindAsync(
            It.IsAny<FilterDefinition<TestResult>>(),
            It.IsAny<FindOptions<TestResult, TestResult>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(cursorMock.Object);

        var result = await _repository.GetByIdAsync(testResult.Id);

        result.Should().BeEquivalentTo(testResult);
        }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnTestResults ()
        {
        var userId = Guid.NewGuid();
        var testResults = new List<TestResult> { new TestResult(userId, TestType.Personality, new Dictionary<string, int>(), 75.0, DateTime.UtcNow) };
        var cursorMock = new Mock<IAsyncCursor<TestResult>>();
        cursorMock.Setup(c => c.Current).Returns(testResults);
        cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
        cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
        _mongoCollectionMock.Setup(m => m.FindAsync(
            It.IsAny<FilterDefinition<TestResult>>(),
            It.IsAny<FindOptions<TestResult, TestResult>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(cursorMock.Object);

        var result = await _repository.GetByUserIdAsync(userId);

        result.Should().BeEquivalentTo(testResults);
        }

    [Fact]
    public async Task UpdateAsync_ShouldReplaceTestResult ()
        {
        var testResult = new TestResult(Guid.NewGuid(), TestType.Personality, new Dictionary<string, int>(), 75.0, DateTime.UtcNow);

        await _repository.UpdateAsync(testResult);

        _mongoCollectionMock.Verify(x => x.ReplaceOneAsync(
            It.IsAny<FilterDefinition<TestResult>>(),
            testResult,
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()), Times.Once());
        }
    }