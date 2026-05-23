using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.TodoLists;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class ToDoListTests : TestBase
    {
        [Fact]
        public async Task List_should_return_todolists()
        {
            // Arrange
            await DbContext.ToDoLists.AddAsync(new ToDoList { Name = "Esimene" });
            await DbContext.ToDoLists.AddAsync(new ToDoList { Name = "Teine" });
            await DbContext.SaveChangesAsync();

            var handler = new ListToDoListsQueryHandler(DbContext);
            var query = new ListToDoListsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count);
        }
    }
}
