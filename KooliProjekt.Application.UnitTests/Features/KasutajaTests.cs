using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Kasutajad;
using Microsoft.EntityFrameworkCore;
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
                Parool = "parool",
                Patsiendid = new List<Patsient>
                {
                    new Patsient { Eesnimi = "P1", Perekonnanimi = "P1", Isikukood = "12345678901", Synniaeg = new DateTime(1990,1,1) }
                }
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
            Assert.Single(result.Value.Patsiendid);
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

        [Fact]
        public async Task List_should_filter_by_search_parameters()
        {
            // Arrange
            await DbContext.Kasutajad.AddAsync(new Kasutaja { Eesnimi = "Mati", Perekonnanimi = "Tamm", Email = "mati@test.ee", Parool = "p1" });
            await DbContext.Kasutajad.AddAsync(new Kasutaja { Eesnimi = "Kati", Perekonnanimi = "Kask", Email = "kati@test.ee", Parool = "p2" });
            await DbContext.SaveChangesAsync();

            var handler = new ListKasutajadQueryHandler(DbContext);
            var query = new ListKasutajadQuery
            {
                Page = 1,
                PageSize = 10,
                Eesnimi = "Mati",
                Perekonnanimi = "Tamm",
                Email = "mati"
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Equal(1, result.Value.RowCount);
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

        // ===== SAVE HANDLER TESTS =====

        [Fact]
        public void Save_should_throw_when_repository_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SaveKasutajaCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            var request = (SaveKasutajaCommand)null;
            var repo = new KasutajaRepository(DbContext);
            var handler = new SaveKasutajaCommandHandler(repo);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_if_id_is_negative()
        {
            var request = new SaveKasutajaCommand { Id = -10 };
            var repo = new KasutajaRepository(GetFaultyDbContext());
            var handler = new SaveKasutajaCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_kasutaja()
        {
            var request = new SaveKasutajaCommand { Id = 0, Eesnimi = "Uus", Perekonnanimi = "Kasutaja", Email = "uus@tervis.ee", Parool = "parool" };
            var repo = new KasutajaRepository(DbContext);
            var handler = new SaveKasutajaCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.Kasutajad.SingleOrDefaultAsync(k => k.Id == 1);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(1, saved.Id);
        }

        [Fact]
        public async Task Save_should_update_existing_kasutaja()
        {
            var existing = new Kasutaja { Eesnimi = "Vana", Perekonnanimi = "Kasutaja", Email = "vana@tervis.ee", Parool = "vanaparool" };
            await DbContext.Kasutajad.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var request = new SaveKasutajaCommand { Id = existing.Id, Eesnimi = "Uuendatud", Perekonnanimi = "Kasutaja", Email = "uue@tervis.ee", Parool = "uusparool" };
            var repo = new KasutajaRepository(DbContext);
            var handler = new SaveKasutajaCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.Kasutajad.SingleOrDefaultAsync(k => k.Id == request.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(request.Eesnimi, saved.Eesnimi);
        }

        [Fact]
        public async Task Save_should_not_update_missing_kasutaja()
        {
            var request = new SaveKasutajaCommand { Id = 999, Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = "parool" };
            var repo = new KasutajaRepository(DbContext);
            var handler = new SaveKasutajaCommandHandler(repo);

            var existing = new Kasutaja { Eesnimi = "Vana", Perekonnanimi = "Kasutaja", Email = "vana@tervis.ee", Parool = "vanaparool" };
            await DbContext.Kasutajad.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        // ===== VALIDATOR TESTS =====

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234567890123456789012345678901234567890123456789000")]
        public void SaveValidator_should_return_false_when_eesnimi_is_invalid(string eesnimi)
        {
            var validator = new SaveKasutajaCommandValidator(DbContext);
            var command = new SaveKasutajaCommand { Id = 0, Eesnimi = eesnimi, Perekonnanimi = "Test", Email = "test@tervis.ee", Parool = "parool" };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveKasutajaCommand.Eesnimi), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234567890123456789012345678901234567890123456789000")]
        public void SaveValidator_should_return_false_when_perekonnanimi_is_invalid(string perekonnanimi)
        {
            var validator = new SaveKasutajaCommandValidator(DbContext);
            var command = new SaveKasutajaCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = perekonnanimi, Email = "test@tervis.ee", Parool = "parool" };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveKasutajaCommand.Perekonnanimi), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SaveValidator_should_return_false_when_email_is_invalid(string email)
        {
            var validator = new SaveKasutajaCommandValidator(DbContext);
            var command = new SaveKasutajaCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = email, Parool = "parool" };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveKasutajaCommand.Email), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234567890123456789012345678901234567890123456789000")]
        public void SaveValidator_should_return_false_when_parool_is_invalid(string parool)
        {
            var validator = new SaveKasutajaCommandValidator(DbContext);
            var command = new SaveKasutajaCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = parool };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveKasutajaCommand.Parool), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_command_is_valid()
        {
            var validator = new SaveKasutajaCommandValidator(DbContext);
            var command = new SaveKasutajaCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = "Kasutaja", Email = "test@tervis.ee", Parool = "parool" };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
