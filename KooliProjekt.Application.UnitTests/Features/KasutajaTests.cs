using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Kasutajad;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class KasutajaTests : TestBase
    {
        // ===== GET TESTS =====

        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetKasutajaQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new GetKasutajaQuery { Id = 1 };
            var kasutaja = new Kasutaja
            {
                Eesnimi = "Test",
                Perekonnanimi = "Kasutaja",
                Email = "test@tervis.ee",
                Parool = "parool"
            };
            var repo = new KasutajaRepository(DbContext);
            var handler = new GetKasutajaQueryHandler(repo);
            await DbContext.Kasutajad.AddAsync(kasutaja);
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
            var query = new GetKasutajaQuery { Id = 101 };
            var kasutaja = new Kasutaja
            {
                Eesnimi = "Test",
                Perekonnanimi = "Kasutaja",
                Email = "test@tervis.ee",
                Parool = "parool"
            };
            var repo = new KasutajaRepository(DbContext);
            var handler = new GetKasutajaQueryHandler(repo);
            await DbContext.Kasutajad.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            // Arrange
            var repo = new KasutajaRepository(DbContext);
            var handler = new GetKasutajaQueryHandler(repo);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(null, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Get_should_return_null_request_id_is_zero_or_less(int id)
        {
            // Arrange
            var dbContext = GetFaultyDbContext();
            var repo = new KasutajaRepository(dbContext);
            var query = new GetKasutajaQuery { Id = id };
            var handler = new GetKasutajaQueryHandler(repo);

            var kasutaja = new Kasutaja { Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = "parool" };
            await DbContext.Kasutajad.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        // ===== LIST TESTS =====

        [Fact]
        public void List_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ListKasutajadQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            // Arrange
            var query = new ListKasutajadQuery { Page = 1, PageSize = 10 };
            var kasutaja = new Kasutaja
            {
                Eesnimi = "Test",
                Perekonnanimi = "Kasutaja",
                Email = "test@tervis.ee",
                Parool = "parool"
            };
            var handler = new ListKasutajadQueryHandler(DbContext);
            await DbContext.Kasutajad.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            // Arrange
            var handler = new ListKasutajadQueryHandler(DbContext);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(null, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task List_throws_if_page_is_zero_or_less(int page)
        {
            // Arrange
            var query = new ListKasutajadQuery { Page = page, PageSize = 10 };
            var handler = new ListKasutajadQueryHandler(DbContext);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task List_throws_if_pagesize_is_zero_or_less(int pageSize)
        {
            // Arrange
            var query = new ListKasutajadQuery { Page = 1, PageSize = pageSize };
            var handler = new ListKasutajadQueryHandler(DbContext);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(101)]
        [InlineData(1000)]
        public async Task List_throws_if_pagesize_is_too_large(int pageSize)
        {
            // Arrange
            var query = new ListKasutajadQuery { Page = 1, PageSize = pageSize };
            var handler = new ListKasutajadQueryHandler(DbContext);

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        // ===== DELETE TESTS =====

        [Fact]
        public void Delete_should_throw_when_dbcontext_is_null()
        {
            var dbContext = (ApplicationDbContext)null;
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new DeleteKasutajaCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (DeleteKasutajaCommand)null;
            var handler = new DeleteKasutajaCommandHandler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Delete_should_return_when_request_id_is_null_or_negative(int id)
        {
            // Arrange
            var query = new DeleteKasutajaCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteKasutajaCommandHandler(faultyDbContext);

            var kasutaja = new Kasutaja { Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = "parool" };
            await DbContext.Kasutajad.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_kasutaja()
        {
            // Arrange
            var query = new DeleteKasutajaCommand { Id = 1 };
            var handler = new DeleteKasutajaCommandHandler(DbContext);

            var kasutaja = new Kasutaja { Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = "parool" };
            await DbContext.Kasutajad.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Kasutajad.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_kasutaja_does_not_exists()
        {
            // Arrange
            var query = new DeleteKasutajaCommand { Id = 101 };
            var handler = new DeleteKasutajaCommandHandler(DbContext);

            var kasutaja = new Kasutaja { Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = "parool" };
            await DbContext.Kasutajad.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Kasutajad.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }
    }
}
