using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Patsiendid;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class PatsientTests : TestBase
    {
        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetPatsientQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetPatsientQuery { Id = 1 };
            var patsient = new Patsient
            {
                Eesnimi = "Test",
                Perekonnanimi = "Patsient",
                Isikukood = "39001011234",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = 1
            };
            var repo = new PatsientRepository(DbContext);
            var handler = new GetPatsientQueryHandler(repo);
            await DbContext.Patsiendid.AddAsync(patsient);
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
            var query = new GetPatsientQuery { Id = 101 };
            var patsient = new Patsient
            {
                Eesnimi = "Test",
                Perekonnanimi = "Patsient",
                Isikukood = "39001011234",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = 1
            };
            var repo = new PatsientRepository(DbContext);
            var handler = new GetPatsientQueryHandler(repo);
            await DbContext.Patsiendid.AddAsync(patsient);
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
            var repo = new PatsientRepository(DbContext);
            var handler = new GetPatsientQueryHandler(repo);

            // Act
            var result = await handler.Handle(null, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
