
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using PNA.Core.Entities;
using PNA.Core.Enums;
using PNA.AuthService.Infrastructure.Data;
using Xunit;

namespace PsychoNeuroAssessment.Tests.AuthService.Infrastructure.Data
    {
    public class MongoUserRepositoryTests
        {
        private readonly Mock<IMongoCollection<User>> _mongoCollectionMock;
        private readonly MongoUserRepository _repository;

        public MongoUserRepositoryTests ()
            {
            _mongoCollectionMock = new Mock<IMongoCollection<User>>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["MongoDB:ConnectionString"]).Returns("mongodb://localhost:27017");
            configurationMock.Setup(c => c["MongoDB:Database"]).Returns("TestDB");
            var clientMock = new Mock<MongoClient>();
            var databaseMock = new Mock<IMongoDatabase>();
            clientMock.Setup(c => c.GetDatabase("TestDB", null)).Returns(databaseMock.Object);
            databaseMock.Setup(d => d.GetCollection<User>("Users", null)).Returns(_mongoCollectionMock.Object);
            _repository = new MongoUserRepository(configurationMock.Object);
            }

        [Fact]
        public async Task AddAsync_ShouldInsertUser ()
            {
            var user = new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);

            await _repository.AddAsync(user);

            _mongoCollectionMock.Verify(x => x.InsertOneAsync(
                It.Is<User>(u => u.Id == user.Id && u.Username == "testuser"),
                null,
                It.IsAny<CancellationToken>()), Times.Once());
            }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser ()
            {
            var user = new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);
            var cursorMock = new Mock<IAsyncCursor<User>>();
            cursorMock.Setup(c => c.Current).Returns(new List<User> { user });
            cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            _mongoCollectionMock.Setup(m => m.FindAsync(
                It.IsAny<FilterDefinition<User>>(),
                It.IsAny<FindOptions<User, User>>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(cursorMock.Object);

            var result = await _repository.GetByIdAsync(user.Id);

            result.Should().BeEquivalentTo(user);
            _mongoCollectionMock.Verify(m => m.FindAsync(
                It.IsAny<FilterDefinition<User>>(),
                It.IsAny<FindOptions<User, User>>(),
                It.IsAny<CancellationToken>()), Times.Once());
            }

        [Fact]
        public async Task GetByUsernameAsync_ShouldReturnUser ()
            {
            var user = new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);
            var cursorMock = new Mock<IAsyncCursor<User>>();
            cursorMock.Setup(c => c.Current).Returns(new List<User> { user });
            cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            _mongoCollectionMock.Setup(m => m.FindAsync(
                It.IsAny<FilterDefinition<User>>(),
                It.IsAny<FindOptions<User, User>>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(cursorMock.Object);

            var result = await _repository.GetByUsernameAsync("testuser");

            result.Should().BeEquivalentTo(user);
            }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers ()
            {
            var users = new List<User> { new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null) };
            var cursorMock = new Mock<IAsyncCursor<User>>();
            cursorMock.Setup(c => c.Current).Returns(users);
            cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
            cursorMock.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
            _mongoCollectionMock.Setup(m => m.FindAsync(
                It.IsAny<FilterDefinition<User>>(),
                It.IsAny<FindOptions<User, User>>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(cursorMock.Object);

            var result = await _repository.GetAllAsync();

            result.Should().BeEquivalentTo(users);
            }

        [Fact]
        public async Task UpdateAsync_ShouldReplaceUser ()
            {
            var user = new User("testuser", "hash", "test@example.com", DateTime.Now, Gender.Male, MaritalStatus.SingleNeverMarried, null, JobStatus.FullTime, null, null, null);

            await _repository.UpdateAsync(user);

            _mongoCollectionMock.Verify(x => x.ReplaceOneAsync(
                It.IsAny<FilterDefinition<User>>(),
                user,
                It.IsAny<ReplaceOptions>(),
                It.IsAny<CancellationToken>()), Times.Once());
            }
        }
    }