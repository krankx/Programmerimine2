using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Toiduained;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        [Fact]
        public async Task List_should_filter_by_search_parameters()
        {
            // Arrange
            await DbContext.Toiduained.AddAsync(new Toiduaine { Nimetus = "Leib", Energia = 250 });
            await DbContext.Toiduained.AddAsync(new Toiduaine { Nimetus = "Sai", Energia = 500 });
            await DbContext.SaveChangesAsync();

            var handler = new ListToiduainedQueryHandler(DbContext);
            var query = new ListToiduainedQuery
            {
                Page = 1,
                PageSize = 10,
                Nimetus = "Leib",
                EnergiaMin = 100,
                EnergiaMax = 300
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

        // ===== SAVE HANDLER TESTS =====

        [Fact]
        public void Save_should_throw_when_repository_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SaveToiduaineCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            var request = (SaveToiduaineCommand)null;
            var repo = new ToiduaineRepository(DbContext);
            var handler = new SaveToiduaineCommandHandler(repo);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_if_id_is_negative()
        {
            var request = new SaveToiduaineCommand { Id = -10 };
            var repo = new ToiduaineRepository(GetFaultyDbContext());
            var handler = new SaveToiduaineCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_toiduaine()
        {
            var request = new SaveToiduaineCommand { Id = 0, Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };
            var repo = new ToiduaineRepository(DbContext);
            var handler = new SaveToiduaineCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.Toiduained.SingleOrDefaultAsync(t => t.Id == 1);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(1, saved.Id);
        }

        [Fact]
        public async Task Save_should_update_existing_toiduaine()
        {
            var existing = new Toiduaine { Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };
            await DbContext.Toiduained.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var request = new SaveToiduaineCommand { Id = existing.Id, Nimetus = "Sai", Energia = 300, Valgud = 9m, Susivesikud = 55, MillestSuhkrud = 5, Rasvad = 3, MillestKullastunud = 0.8m, Kiudained = 4, Sool = 1m };
            var repo = new ToiduaineRepository(DbContext);
            var handler = new SaveToiduaineCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.Toiduained.SingleOrDefaultAsync(t => t.Id == request.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(request.Nimetus, saved.Nimetus);
        }

        [Fact]
        public async Task Save_should_not_update_missing_toiduaine()
        {
            var request = new SaveToiduaineCommand { Id = 999, Nimetus = "Leib", Energia = 250 };
            var repo = new ToiduaineRepository(DbContext);
            var handler = new SaveToiduaineCommandHandler(repo);

            var existing = new Toiduaine { Nimetus = "Sai", Energia = 300, Valgud = 9m, Susivesikud = 55, MillestSuhkrud = 5, Rasvad = 3, MillestKullastunud = 0.8m, Kiudained = 4, Sool = 1m };
            await DbContext.Toiduained.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        // ===== VALIDATOR TESTS =====

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SaveValidator_should_return_false_when_nimetus_is_invalid(string nimetus)
        {
            var validator = new SaveToiduaineCommandValidator(DbContext);
            var command = new SaveToiduaineCommand { Id = 0, Nimetus = nimetus, Energia = 250 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveToiduaineCommand.Nimetus), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void SaveValidator_should_return_false_when_energia_is_invalid(decimal energia)
        {
            var validator = new SaveToiduaineCommandValidator(DbContext);
            var command = new SaveToiduaineCommand { Id = 0, Nimetus = "Leib", Energia = energia };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveToiduaineCommand.Energia), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_command_is_valid()
        {
            var validator = new SaveToiduaineCommandValidator(DbContext);
            var command = new SaveToiduaineCommand { Id = 0, Nimetus = "Leib", Energia = 250, Valgud = 8.5m, Susivesikud = 50, MillestSuhkrud = 3, Rasvad = 2, MillestKullastunud = 0.5m, Kiudained = 6, Sool = 1.2m };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
