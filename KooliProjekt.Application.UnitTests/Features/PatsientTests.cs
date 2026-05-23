using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Patsiendid;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class PatsientTests : TestBase
    {
        // ===== GET TESTS =====

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

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
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

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            var repo = new PatsientRepository(DbContext);
            var handler = new GetPatsientQueryHandler(repo);

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
            var dbContext = GetFaultyDbContext();
            var repo = new PatsientRepository(dbContext);
            var query = new GetPatsientQuery { Id = id };
            var handler = new GetPatsientQueryHandler(repo);

            var patsient = new Patsient { Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            await DbContext.Patsiendid.AddAsync(patsient);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

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
                new ListPatsiendidQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            var query = new ListPatsiendidQuery { Page = 1, PageSize = 10 };
            var patsient = new Patsient
            {
                Eesnimi = "Test",
                Perekonnanimi = "Patsient",
                Isikukood = "39001011234",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = 1
            };
            var handler = new ListPatsiendidQueryHandler(DbContext);
            await DbContext.Patsiendid.AddAsync(patsient);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            var handler = new ListPatsiendidQueryHandler(DbContext);

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
            var query = new ListPatsiendidQuery { Page = page, PageSize = 10 };
            var handler = new ListPatsiendidQueryHandler(DbContext);

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
            var query = new ListPatsiendidQuery { Page = 1, PageSize = pageSize };
            var handler = new ListPatsiendidQueryHandler(DbContext);

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
            var query = new ListPatsiendidQuery { Page = 1, PageSize = pageSize };
            var handler = new ListPatsiendidQueryHandler(DbContext);

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
                new DeletePatsientCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            var request = (DeletePatsientCommand)null;
            var handler = new DeletePatsientCommandHandler(DbContext);

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
            var query = new DeletePatsientCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeletePatsientCommandHandler(faultyDbContext);

            var patsient = new Patsient { Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            await DbContext.Patsiendid.AddAsync(patsient);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_patsient()
        {
            var query = new DeletePatsientCommand { Id = 1 };
            var handler = new DeletePatsientCommandHandler(DbContext);

            var patsient = new Patsient { Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            await DbContext.Patsiendid.AddAsync(patsient);
            await DbContext.KaaluMootmised.AddAsync(new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 });
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Patsiendid.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_patsient_does_not_exists()
        {
            var query = new DeletePatsientCommand { Id = 101 };
            var handler = new DeletePatsientCommandHandler(DbContext);

            var patsient = new Patsient { Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            await DbContext.Patsiendid.AddAsync(patsient);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Patsiendid.FindAsync(query.Id);

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
                new SavePatsientCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            var request = (SavePatsientCommand)null;
            var repo = new PatsientRepository(DbContext);
            var handler = new SavePatsientCommandHandler(repo);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_if_id_is_negative()
        {
            var request = new SavePatsientCommand { Id = -10 };
            var repo = new PatsientRepository(GetFaultyDbContext());
            var handler = new SavePatsientCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_patsient()
        {
            var request = new SavePatsientCommand { Id = 0, Eesnimi = "Uus", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            var repo = new PatsientRepository(DbContext);
            var handler = new SavePatsientCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.Patsiendid.SingleOrDefaultAsync(p => p.Id == 1);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(1, saved.Id);
        }

        [Fact]
        public async Task Save_should_update_existing_patsient()
        {
            var existing = new Patsient { Eesnimi = "Vana", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            await DbContext.Patsiendid.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var request = new SavePatsientCommand { Id = existing.Id, Eesnimi = "Uuendatud", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            var repo = new PatsientRepository(DbContext);
            var handler = new SavePatsientCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.Patsiendid.SingleOrDefaultAsync(p => p.Id == request.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(request.Eesnimi, saved.Eesnimi);
        }

        [Fact]
        public async Task Save_should_not_update_missing_patsient()
        {
            var request = new SavePatsientCommand { Id = 999, Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            var repo = new PatsientRepository(DbContext);
            var handler = new SavePatsientCommandHandler(repo);

            var existing = new Patsient { Eesnimi = "Vana", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };
            await DbContext.Patsiendid.AddAsync(existing);
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
            var validator = new SavePatsientCommandValidator();
            var command = new SavePatsientCommand { Id = 0, Eesnimi = eesnimi, Perekonnanimi = "Test", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SavePatsientCommand.Eesnimi), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234567890123456789012345678901234567890123456789000")]
        public void SaveValidator_should_return_false_when_perekonnanimi_is_invalid(string perekonnanimi)
        {
            var validator = new SavePatsientCommandValidator();
            var command = new SavePatsientCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = perekonnanimi, Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SavePatsientCommand.Perekonnanimi), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("1234567890")]
        [InlineData("123456789012")]
        public void SaveValidator_should_return_false_when_isikukood_is_invalid(string isikukood)
        {
            var validator = new SavePatsientCommandValidator();
            var command = new SavePatsientCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = isikukood, Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SavePatsientCommand.Isikukood), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SaveValidator_should_return_false_when_kasutaja_id_is_invalid(int kasutajaId)
        {
            var validator = new SavePatsientCommandValidator();
            var command = new SavePatsientCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = kasutajaId };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SavePatsientCommand.KasutajaId), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_command_is_valid()
        {
            var validator = new SavePatsientCommandValidator();
            var command = new SavePatsientCommand { Id = 0, Eesnimi = "Test", Perekonnanimi = "Patsient", Isikukood = "39001011234", Synniaeg = new DateTime(1990, 1, 1), KasutajaId = 1 };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
