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
    }
}
