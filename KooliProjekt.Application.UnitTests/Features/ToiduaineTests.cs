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
        // ===== GET TESTS =====

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

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
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

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            var repo = new ToiduaineRepository(DbContext);
            var handler = new GetToiduaineQueryHandler(repo);

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
            var repo = new ToiduaineRepository(dbContext);
            var query = new GetToiduaineQuery { Id = id };
            var handler = new GetToiduaineQueryHandler(repo);

            var toiduaine = new Toiduaine { Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };
            await DbContext.Toiduained.AddAsync(toiduaine);
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
                new ListToiduainedQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            var query = new ListToiduainedQuery { Page = 1, PageSize = 10 };
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
            var handler = new ListToiduainedQueryHandler(DbContext);
            await DbContext.Toiduained.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            var handler = new ListToiduainedQueryHandler(DbContext);

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
            var query = new ListToiduainedQuery { Page = page, PageSize = 10 };
            var handler = new ListToiduainedQueryHandler(DbContext);

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
            var query = new ListToiduainedQuery { Page = 1, PageSize = pageSize };
            var handler = new ListToiduainedQueryHandler(DbContext);

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
            var query = new ListToiduainedQuery { Page = 1, PageSize = pageSize };
            var handler = new ListToiduainedQueryHandler(DbContext);

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
                new DeleteToiduaineCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            var request = (DeleteToiduaineCommand)null;
            var handler = new DeleteToiduaineCommandHandler(DbContext);

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
            var query = new DeleteToiduaineCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteToiduaineCommandHandler(faultyDbContext);

            var toiduaine = new Toiduaine { Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };
            await DbContext.Toiduained.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_toiduaine()
        {
            var query = new DeleteToiduaineCommand { Id = 1 };
            var handler = new DeleteToiduaineCommandHandler(DbContext);

            var toiduaine = new Toiduaine { Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };
            await DbContext.Toiduained.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Toiduained.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_toiduaine_does_not_exists()
        {
            var query = new DeleteToiduaineCommand { Id = 101 };
            var handler = new DeleteToiduaineCommandHandler(DbContext);

            var toiduaine = new Toiduaine { Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };
            await DbContext.Toiduained.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Toiduained.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }
    }
}
