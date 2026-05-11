using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.VererohuMootmised;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class VererohuMootmineTests : TestBase
    {
        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetVererohuMootmineQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetVererohuMootmineQuery { Id = 1 };
            var mootmine = new VererohuMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kellaaeg = new TimeSpan(9, 0, 0),
                Sustoolne = 120,
                Diastoolne = 80,
                Pulss = 72,
                PatsientId = 1
            };
            var repo = new VererohuMootmineRepository(DbContext);
            var handler = new GetVererohuMootmineQueryHandler(repo);
            await DbContext.VererohuMootmised.AddAsync(mootmine);
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
            var query = new GetVererohuMootmineQuery { Id = 101 };
            var mootmine = new VererohuMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kellaaeg = new TimeSpan(9, 0, 0),
                Sustoolne = 120,
                Diastoolne = 80,
                Pulss = 72,
                PatsientId = 1
            };
            var repo = new VererohuMootmineRepository(DbContext);
            var handler = new GetVererohuMootmineQueryHandler(repo);
            await DbContext.VererohuMootmised.AddAsync(mootmine);
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
            var repo = new VererohuMootmineRepository(DbContext);
            var handler = new GetVererohuMootmineQueryHandler(repo);

            // Act
            var result = await handler.Handle(null, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
