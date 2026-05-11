using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.KaaluMootmised;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class KaaluMootmineTests : TestBase
    {
        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetKaaluMootmineQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetKaaluMootmineQuery { Id = 1 };
            var mootmine = new KaaluMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kaal = 75.5m,
                PatsientId = 1
            };
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new GetKaaluMootmineQueryHandler(repo);
            await DbContext.KaaluMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new GetKaaluMootmineQuery { Id = 101 };
            var mootmine = new KaaluMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kaal = 75.5m,
                PatsientId = 1
            };
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new GetKaaluMootmineQueryHandler(repo);
            await DbContext.KaaluMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_null_if_query_is_null()
        {
            // Arrange
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new GetKaaluMootmineQueryHandler(repo);

            // Act
            var result = await handler.Handle(null, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
