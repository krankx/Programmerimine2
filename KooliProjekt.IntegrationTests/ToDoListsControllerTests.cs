using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class TodoListsControllerTests : TestBase
    {
        [Fact]
        public async Task List_should_return_list()
        {
            // Arrange
            var url = "/api/ToDoLists";

            var todoList = new ToDoList { Name = "Test List" };
            await DbContext.AddAsync(todoList);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<IList<ToDoList>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }
    }
}