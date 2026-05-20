using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.VeresuhkruMootmised;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class VeresuhkruMootmineTests : TestBase
    {
        // ===== GET TESTS =====

        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetVeresuhkruMootmineQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            var query = new GetVeresuhkruMootmineQuery { Id = 1 };
            var mootmine = new VeresuhkruMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kellaaeg = new TimeSpan(8, 0, 0),
                Veresuhkur = 5.4m,
                PatsientId = 1
            };
            var repo = new VeresuhkruMootmineRepository(DbContext);
            var handler = new GetVeresuhkruMootmineQueryHandler(repo);
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            var query = new GetVeresuhkruMootmineQuery { Id = 101 };
            var mootmine = new VeresuhkruMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kellaaeg = new TimeSpan(8, 0, 0),
                Veresuhkur = 5.4m,
                PatsientId = 1
            };
            var repo = new VeresuhkruMootmineRepository(DbContext);
            var handler = new GetVeresuhkruMootmineQueryHandler(repo);
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            var repo = new VeresuhkruMootmineRepository(DbContext);
            var handler = new GetVeresuhkruMootmineQueryHandler(repo);

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
            var repo = new VeresuhkruMootmineRepository(dbContext);
            var query = new GetVeresuhkruMootmineQuery { Id = id };
            var handler = new GetVeresuhkruMootmineQueryHandler(repo);

            var mootmine = new VeresuhkruMootmine { Kuupaev = new DateTime(2025, 10, 1), Kellaaeg = new TimeSpan(8, 0, 0), Veresuhkur = 5.4m, PatsientId = 1 };
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
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
                new ListVeresuhkruMootmisedQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            var query = new ListVeresuhkruMootmisedQuery { Page = 1, PageSize = 10 };
            var mootmine = new VeresuhkruMootmine
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Kellaaeg = new TimeSpan(8, 0, 0),
                Veresuhkur = 5.4m,
                PatsientId = 1
            };
            var handler = new ListVeresuhkruMootmisedQueryHandler(DbContext);
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            var handler = new ListVeresuhkruMootmisedQueryHandler(DbContext);

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
            var query = new ListVeresuhkruMootmisedQuery { Page = page, PageSize = 10 };
            var handler = new ListVeresuhkruMootmisedQueryHandler(DbContext);

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
            var query = new ListVeresuhkruMootmisedQuery { Page = 1, PageSize = pageSize };
            var handler = new ListVeresuhkruMootmisedQueryHandler(DbContext);

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
            var query = new ListVeresuhkruMootmisedQuery { Page = 1, PageSize = pageSize };
            var handler = new ListVeresuhkruMootmisedQueryHandler(DbContext);

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
                new DeleteVeresuhkruMootmineCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            var request = (DeleteVeresuhkruMootmineCommand)null;
            var handler = new DeleteVeresuhkruMootmineCommandHandler(DbContext);

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
            var query = new DeleteVeresuhkruMootmineCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteVeresuhkruMootmineCommandHandler(faultyDbContext);

            var mootmine = new VeresuhkruMootmine { Kuupaev = new DateTime(2025, 10, 1), Kellaaeg = new TimeSpan(8, 0, 0), Veresuhkur = 5.4m, PatsientId = 1 };
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_veresuhkru_mootmine()
        {
            var query = new DeleteVeresuhkruMootmineCommand { Id = 1 };
            var handler = new DeleteVeresuhkruMootmineCommandHandler(DbContext);

            var mootmine = new VeresuhkruMootmine { Kuupaev = new DateTime(2025, 10, 1), Kellaaeg = new TimeSpan(8, 0, 0), Veresuhkur = 5.4m, PatsientId = 1 };
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.VeresuhkruMootmised.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_veresuhkru_mootmine_does_not_exists()
        {
            var query = new DeleteVeresuhkruMootmineCommand { Id = 101 };
            var handler = new DeleteVeresuhkruMootmineCommandHandler(DbContext);

            var mootmine = new VeresuhkruMootmine { Kuupaev = new DateTime(2025, 10, 1), Kellaaeg = new TimeSpan(8, 0, 0), Veresuhkur = 5.4m, PatsientId = 1 };
            await DbContext.VeresuhkruMootmised.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.VeresuhkruMootmised.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }
    }
}
