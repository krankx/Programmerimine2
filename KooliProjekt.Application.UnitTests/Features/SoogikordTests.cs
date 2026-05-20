using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Soogikorrad;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class SoogikordTests : TestBase
    {
        // ===== GET TESTS =====

        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetSoogikordQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            var query = new GetSoogikordQuery { Id = 1 };
            var soogikord = new Soogikord
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Tyyp = SoogikorraTyyp.Hommikusook,
                PatsientId = 1
            };
            var repo = new SoogikordRepository(DbContext);
            var handler = new GetSoogikordQueryHandler(repo);
            await DbContext.Soogikorrad.AddAsync(soogikord);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            var query = new GetSoogikordQuery { Id = 101 };
            var soogikord = new Soogikord
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Tyyp = SoogikorraTyyp.Hommikusook,
                PatsientId = 1
            };
            var repo = new SoogikordRepository(DbContext);
            var handler = new GetSoogikordQueryHandler(repo);
            await DbContext.Soogikorrad.AddAsync(soogikord);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            var repo = new SoogikordRepository(DbContext);
            var handler = new GetSoogikordQueryHandler(repo);

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
            var repo = new SoogikordRepository(dbContext);
            var query = new GetSoogikordQuery { Id = id };
            var handler = new GetSoogikordQueryHandler(repo);

            var soogikord = new Soogikord { Kuupaev = new DateTime(2025, 10, 1), Tyyp = SoogikorraTyyp.Hommikusook, PatsientId = 1 };
            await DbContext.Soogikorrad.AddAsync(soogikord);
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
                new ListSoogikorradQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            var query = new ListSoogikorradQuery { Page = 1, PageSize = 10 };
            var soogikord = new Soogikord
            {
                Kuupaev = new DateTime(2025, 10, 1),
                Tyyp = SoogikorraTyyp.Hommikusook,
                PatsientId = 1
            };
            var handler = new ListSoogikorradQueryHandler(DbContext);
            await DbContext.Soogikorrad.AddAsync(soogikord);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            var handler = new ListSoogikorradQueryHandler(DbContext);

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
            var query = new ListSoogikorradQuery { Page = page, PageSize = 10 };
            var handler = new ListSoogikorradQueryHandler(DbContext);

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
            var query = new ListSoogikorradQuery { Page = 1, PageSize = pageSize };
            var handler = new ListSoogikorradQueryHandler(DbContext);

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
            var query = new ListSoogikorradQuery { Page = 1, PageSize = pageSize };
            var handler = new ListSoogikorradQueryHandler(DbContext);

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
                new DeleteSoogikordCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            var request = (DeleteSoogikordCommand)null;
            var handler = new DeleteSoogikordCommandHandler(DbContext);

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
            var query = new DeleteSoogikordCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteSoogikordCommandHandler(faultyDbContext);

            var soogikord = new Soogikord { Kuupaev = new DateTime(2025, 10, 1), Tyyp = SoogikorraTyyp.Hommikusook, PatsientId = 1 };
            await DbContext.Soogikorrad.AddAsync(soogikord);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_soogikord()
        {
            var query = new DeleteSoogikordCommand { Id = 1 };
            var handler = new DeleteSoogikordCommandHandler(DbContext);

            var soogikord = new Soogikord { Kuupaev = new DateTime(2025, 10, 1), Tyyp = SoogikorraTyyp.Hommikusook, PatsientId = 1, Read = new List<SoogikorraRida>() };
            soogikord.Read.Add(new SoogikorraRida { Kogus = 100, ToiduaineId = 1 });
            soogikord.Read.Add(new SoogikorraRida { Kogus = 200, ToiduaineId = 2 });

            await DbContext.Soogikorrad.AddAsync(soogikord);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Soogikorrad.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_soogikord_does_not_exists()
        {
            var query = new DeleteSoogikordCommand { Id = 101 };
            var handler = new DeleteSoogikordCommandHandler(DbContext);

            var soogikord = new Soogikord { Kuupaev = new DateTime(2025, 10, 1), Tyyp = SoogikorraTyyp.Hommikusook, PatsientId = 1 };
            await DbContext.Soogikorrad.AddAsync(soogikord);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.Soogikorrad.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }
    }
}
