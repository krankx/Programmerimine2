using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.SoogikorraRead;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class SoogikorraRidaTests : TestBase
    {
        // ===== GET TESTS =====

        [Fact]
        public void Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetSoogikorraRidaQueryHandler(null);
            });
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            var query = new GetSoogikorraRidaQuery { Id = 1 };
            var rida = new SoogikorraRida
            {
                Kogus = 100,
                SoogikordId = 1,
                ToiduaineId = 1
            };
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new GetSoogikorraRidaQueryHandler(repo);
            await DbContext.SoogikorraRead.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            var query = new GetSoogikorraRidaQuery { Id = 101 };
            var rida = new SoogikorraRida
            {
                Kogus = 100,
                SoogikordId = 1,
                ToiduaineId = 1
            };
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new GetSoogikorraRidaQueryHandler(repo);
            await DbContext.SoogikorraRead.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_throws_if_request_is_null()
        {
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new GetSoogikorraRidaQueryHandler(repo);

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
            var repo = new SoogikorraRidaRepository(dbContext);
            var query = new GetSoogikorraRidaQuery { Id = id };
            var handler = new GetSoogikorraRidaQueryHandler(repo);

            var rida = new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            await DbContext.SoogikorraRead.AddAsync(rida);
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
                new ListSoogikorraReadQueryHandler(null);
            });
        }

        [Fact]
        public async Task List_should_return_objects_if_objects_exist()
        {
            var query = new ListSoogikorraReadQuery { Page = 1, PageSize = 10 };
            var rida = new SoogikorraRida
            {
                Kogus = 100,
                SoogikordId = 1,
                ToiduaineId = 1
            };
            var handler = new ListSoogikorraReadQueryHandler(DbContext);
            await DbContext.SoogikorraRead.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.RowCount);
        }

        [Fact]
        public async Task List_throws_if_request_is_null()
        {
            var handler = new ListSoogikorraReadQueryHandler(DbContext);

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
            var query = new ListSoogikorraReadQuery { Page = page, PageSize = 10 };
            var handler = new ListSoogikorraReadQueryHandler(DbContext);

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
            var query = new ListSoogikorraReadQuery { Page = 1, PageSize = pageSize };
            var handler = new ListSoogikorraReadQueryHandler(DbContext);

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
            var query = new ListSoogikorraReadQuery { Page = 1, PageSize = pageSize };
            var handler = new ListSoogikorraReadQueryHandler(DbContext);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task List_should_filter_by_search_parameters()
        {
            // Arrange
            await DbContext.SoogikorraRead.AddAsync(new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 });
            await DbContext.SoogikorraRead.AddAsync(new SoogikorraRida { Kogus = 200, SoogikordId = 2, ToiduaineId = 2 });
            await DbContext.SaveChangesAsync();

            var handler = new ListSoogikorraReadQueryHandler(DbContext);
            var query = new ListSoogikorraReadQuery
            {
                Page = 1,
                PageSize = 10,
                SoogikordId = 1,
                ToiduaineId = 1
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
                new DeleteSoogikorraRidaCommandHandler(dbContext);
            });

            Assert.Equal(nameof(dbContext), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            var request = (DeleteSoogikorraRidaCommand)null;
            var handler = new DeleteSoogikorraRidaCommandHandler(DbContext);

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
            var query = new DeleteSoogikorraRidaCommand { Id = id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new DeleteSoogikorraRidaCommandHandler(faultyDbContext);

            var rida = new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            await DbContext.SoogikorraRead.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_should_remove_existing_soogikorra_rida()
        {
            var query = new DeleteSoogikorraRidaCommand { Id = 1 };
            var handler = new DeleteSoogikorraRidaCommandHandler(DbContext);

            var rida = new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            await DbContext.SoogikorraRead.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.SoogikorraRead.FindAsync(query.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(test);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_soogikorra_rida_does_not_exists()
        {
            var query = new DeleteSoogikorraRidaCommand { Id = 101 };
            var handler = new DeleteSoogikorraRidaCommandHandler(DbContext);

            var rida = new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            await DbContext.SoogikorraRead.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var result = await handler.Handle(query, CancellationToken.None);
            var test = await DbContext.SoogikorraRead.FindAsync(query.Id);

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
                new SaveSoogikorraRidaCommandHandler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            var request = (SaveSoogikorraRidaCommand)null;
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new SaveSoogikorraRidaCommandHandler(repo);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_if_id_is_negative()
        {
            var request = new SaveSoogikorraRidaCommand { Id = -10 };
            var repo = new SoogikorraRidaRepository(GetFaultyDbContext());
            var handler = new SaveSoogikorraRidaCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_soogikorra_rida()
        {
            var request = new SaveSoogikorraRidaCommand { Id = 0, Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new SaveSoogikorraRidaCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.SoogikorraRead.SingleOrDefaultAsync(r => r.Id == 1);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(1, saved.Id);
        }

        [Fact]
        public async Task Save_should_update_existing_soogikorra_rida()
        {
            var existing = new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            await DbContext.SoogikorraRead.AddAsync(existing);
            await DbContext.SaveChangesAsync();

            var request = new SaveSoogikorraRidaCommand { Id = existing.Id, Kogus = 200, SoogikordId = 1, ToiduaineId = 2 };
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new SaveSoogikorraRidaCommandHandler(repo);

            var result = await handler.Handle(request, CancellationToken.None);
            var saved = await DbContext.SoogikorraRead.SingleOrDefaultAsync(r => r.Id == request.Id);

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(saved);
            Assert.Equal(request.Kogus, saved.Kogus);
        }

        [Fact]
        public async Task Save_should_not_update_missing_soogikorra_rida()
        {
            var request = new SaveSoogikorraRidaCommand { Id = 999, Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            var repo = new SoogikorraRidaRepository(DbContext);
            var handler = new SaveSoogikorraRidaCommandHandler(repo);

            var existing = new SoogikorraRida { Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };
            await DbContext.SoogikorraRead.AddAsync(existing);
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
        public void SaveValidator_should_return_false_when_kogus_is_invalid(decimal kogus)
        {
            var validator = new SaveSoogikorraRidaCommandValidator(DbContext);
            var command = new SaveSoogikorraRidaCommand { Id = 0, Kogus = kogus, SoogikordId = 1, ToiduaineId = 1 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveSoogikorraRidaCommand.Kogus), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SaveValidator_should_return_false_when_soogikord_id_is_invalid(int soogikordId)
        {
            var validator = new SaveSoogikorraRidaCommandValidator(DbContext);
            var command = new SaveSoogikorraRidaCommand { Id = 0, Kogus = 100, SoogikordId = soogikordId, ToiduaineId = 1 };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveSoogikorraRidaCommand.SoogikordId), result.Errors.First().PropertyName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SaveValidator_should_return_false_when_toiduaine_id_is_invalid(int toiduaineId)
        {
            var validator = new SaveSoogikorraRidaCommandValidator(DbContext);
            var command = new SaveSoogikorraRidaCommand { Id = 0, Kogus = 100, SoogikordId = 1, ToiduaineId = toiduaineId };

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Equal(nameof(SaveSoogikorraRidaCommand.ToiduaineId), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_command_is_valid()
        {
            var validator = new SaveSoogikorraRidaCommandValidator(DbContext);
            var command = new SaveSoogikorraRidaCommand { Id = 0, Kogus = 100, SoogikordId = 1, ToiduaineId = 1 };

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
