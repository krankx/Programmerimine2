using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Toiduained;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class ToiduaineTests : TestBase
    {
        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetToiduaineQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetToiduaineQuery { Id = 1 };
            var toiduaine = new Toiduaine
            {
                Nimetus = "Leib",
                Energia = 250,
                Valgud = 8.5m,
                Susivesikud = 50,
                MillestSuhkrud = 3,
                Rasvad = 2,
                MillestKullastunud = 0.5m,
                Kiudained = 6,
                Sool = 1.2m
            };
            var repo = new ToiduaineRepository(DbContext);
            var handler = new GetToiduaineQueryHandler(repo);
            await DbContext.Toiduained.AddAsync(toiduaine);
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
            var query = new GetToiduaineQuery { Id = 101 };
            var toiduaine = new Toiduaine
            {
                Nimetus = "Leib",
                Energia = 250,
                Valgud = 8.5m,
                Susivesikud = 50,
                MillestSuhkrud = 3,
                Rasvad = 2,
                MillestKullastunud = 0.5m,
                Kiudained = 6,
                Sool = 1.2m
            };
            var repo = new ToiduaineRepository(DbContext);
            var handler = new GetToiduaineQueryHandler(repo);
            await DbContext.Toiduained.AddAsync(toiduaine);
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
            var repo = new ToiduaineRepository(DbContext);
            var handler = new GetToiduaineQueryHandler(repo);

            // Act
            var result = await handler.Handle(null, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
