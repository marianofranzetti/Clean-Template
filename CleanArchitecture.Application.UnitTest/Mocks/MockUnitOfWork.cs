using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {

            var options = new DbContextOptionsBuilder<StreamerDbContext>()
              .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{Guid.NewGuid()}")
              .Options;

            var streamerDbContextFake = new StreamerDbContext(options);

            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);

            streamerDbContextFake.Database.EnsureDeleted();

            return mockUnitOfWork;
        }
    }
}
