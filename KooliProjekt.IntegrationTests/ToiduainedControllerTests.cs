using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Features.Toiduained;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class ToiduainedControllerTests : TestBase
    {
        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/Toiduained/List/?page=1&pageSize=10";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<Toiduaine>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_toiduaine()
        {
            // Arrange
            var toiduaine = new Toiduaine
            {
                Nimetus = "Leib",
                Energia = 250m,
                Valgud = 9m,
                Susivesikud = 45m,
                MillestSuhkrud = 3m,
                Rasvad = 3m,
                MillestKullastunud = 1m,
                Kiudained = 6m,
                Sool = 1m
            };
            await DbContext.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            var url = "/api/Toiduained/Get/?id=" + toiduaine.Id;

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<ToiduaineDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_toiduaine()
        {
            // Arrange
            var url = "/api/Toiduained/Get/?id=999";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_toiduaine()
        {
            // Arrange
            var url = "/api/Toiduained/Delete/";
            var toiduaine = new Toiduaine
            {
                Nimetus = "Leib",
                Energia = 250m
            };
            await DbContext.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = toiduaine.Id })
            };
            using var response = await Client.SendAsync(request);
            var fromDb = await DbContext.Toiduained
                .Where(t => t.Id == toiduaine.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_shouldwork_with_missing_toiduaine()
        {
            // Arrange
            var url = "/api/Toiduained/Delete/";

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = 999 })
            };
            using var response = await Client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_add_new_toiduaine()
        {
            // Arrange
            var url = "/api/Toiduained/Save/";
            var command = new SaveToiduaineCommand
            {
                Nimetus = "Leib",
                Energia = 250m,
                Valgud = 9m,
                Susivesikud = 45m,
                MillestSuhkrud = 3m,
                Rasvad = 3m,
                MillestKullastunud = 1m,
                Kiudained = 6m,
                Sool = 1m
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.Toiduained
                .Where(t => t.Nimetus == "Leib")
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_missing_toiduaine()
        {
            // Arrange
            var url = "/api/Toiduained/Save/";
            var command = new SaveToiduaineCommand
            {
                Id = 999,
                Nimetus = "Leib",
                Energia = 250m
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.Toiduained
                .Where(t => t.Id == 999)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_invalid_toiduaine()
        {
            // Arrange
            var url = "/api/Toiduained/Save/";
            var command = new SaveToiduaineCommand
            {
                Id = 0,
                Nimetus = "",
                Energia = -1m
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }
    }
}
