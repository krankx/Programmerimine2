using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.KaaluMootmised;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class KaaluMootmineTests : TestBase
    {
        // ===== GET TESTS =====

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

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
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

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new GetKaaluMootmineQueryHandler(repo);

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
            var repo = new KaaluMootmineRepository(dbContext);
            var query = new GetKaaluMootmineQuery { Id = id };
            var handler = new GetKaaluMootmineQueryHandler(repo);

            var mootmine = new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            await DbContext.KaaluMootmised.AddAsync(mootmine);
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
                new ListKaaluMootmisedQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            var query = new ListKaaluMootmisedQuery { Page = 1, PageSize = 10 };
            var mootmine = new KaaluMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kaal = 75.5m,
                PatsientId = 1
            };
            var handler = new ListKaaluMootmisedQueryHandler(DbContext);
            await DbContext.KaaluMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            var handler = new ListKaaluMootmisedQueryHandler(DbContext);

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
            var query = new ListKaaluMootmisedQuery { Page = page, PageSize = 10 };
            var handler = new ListKaaluMootmisedQueryHandler(DbContext);

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
            var query = new ListKaaluMootmisedQuery { Page = 1, PageSize = pageSize };
            var handler = new ListKaaluMootmisedQueryHandler(DbContext);

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
            var query = new ListKaaluMootmisedQuery { Page = 1, PageSize = pageSize };
            var handler = new ListKaaluMootmisedQueryHandler(DbContext);

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
                new DeleteKaaluMootmineCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            var request = (DeleteKaaluMootmineCommand)null;
            var handler = new DeleteKaaluMootmineCommandHandler(DbContext);

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
            var query = new DeleteKaaluMootmineCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteKaaluMootmineCommandHandler(faultyDbContext);

            var mootmine = new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            await DbContext.KaaluMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_kaalu_mootmine()
        {
            var query = new DeleteKaaluMootmineCommand { Id = 1 };
            var handler = new DeleteKaaluMootmineCommandHandler(DbContext);

            var mootmine = new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            await DbContext.KaaluMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.KaaluMootmised.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_kaalu_mootmine_does_not_exists()
        {
            var query = new DeleteKaaluMootmineCommand { Id = 101 };
            var handler = new DeleteKaaluMootmineCommandHandler(DbContext);

            var mootmine = new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            await DbContext.KaaluMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.KaaluMootmised.FindAsync(query.Id);

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
                new SaveKaaluMootmineCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            var request = (SaveKaaluMootmineCommand)null;
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new SaveKaaluMootmineCommandHandler(repo);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_if_id_is_negative()
        {
            var request = new SaveKaaluMootmineCommand { Id = -10 };
            var repo = new KaaluMootmineRepository(GetFaultyDbContext());
            var handler = new SaveKaaluMootmineCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_kaalu_mootmine()
        {
            var request = new SaveKaaluMootmineCommand { Id = 0, Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new SaveKaaluMootmineCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.KaaluMootmised.SingleOrDefaultAsync(m => m.Id == 1);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(1, saved.Id);
        }

        [Fact]
        public async Task Save_should_update_existing_kaalu_mootmine()
        {
            var existing = new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            await DbContext.KaaluMootmised.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var request = new SaveKaaluMootmineCommand { Id = existing.Id, Kuupaev = new DateTime(2025, 10, 2), Kaal = 80m, PatsientId = 1 };
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new SaveKaaluMootmineCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.KaaluMootmised.SingleOrDefaultAsync(m => m.Id == request.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(request.Kaal, saved.Kaal);
        }

        [Fact]
        public async Task Save_should_not_update_missing_kaalu_mootmine()
        {
            var request = new SaveKaaluMootmineCommand { Id = 999, Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            var repo = new KaaluMootmineRepository(DbContext);
            var handler = new SaveKaaluMootmineCommandHandler(repo);

            var existing = new KaaluMootmine { Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };
            await DbContext.KaaluMootmised.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        // ===== VALIDATOR TESTS =====

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void SaveValidator_should_return_false_when_kaal_is_invalid(decimal kaal)
        {
            var validator = new SaveKaaluMootmineCommandValidator();
            var command = new SaveKaaluMootmineCommand { Id = 0, Kuupaev = new DateTime(2025, 10, 1), Kaal = kaal, PatsientId = 1 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveKaaluMootmineCommand.Kaal), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SaveValidator_should_return_false_when_patsient_id_is_invalid(int patsientId)
        {
            var validator = new SaveKaaluMootmineCommandValidator();
            var command = new SaveKaaluMootmineCommand { Id = 0, Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = patsientId };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveKaaluMootmineCommand.PatsientId), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_command_is_valid()
        {
            var validator = new SaveKaaluMootmineCommandValidator();
            var command = new SaveKaaluMootmineCommand { Id = 0, Kuupaev = new DateTime(2025, 10, 1), Kaal = 75.5m, PatsientId = 1 };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
