using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Features.Kasutajad;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class KasutajadControllerTests : TestBase
    {
        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/Kasutajad/List/?page=1&pageSize=10";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<Kasutaja>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_kasutaja()
        {
            // Arrange
            var kasutaja = new Kasutaja
            {
                Eesnimi = "Mati",
                Perekonnanimi = "Tamm",
                Email = "mati@test.ee",
                Parool = "parool123"
            };
            await DbContext.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            var url = "/api/Kasutajad/Get/?id=" + kasutaja.Id;

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<KasutajaDetailsDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_kasutaja()
        {
            // Arrange
            var url = "/api/Kasutajad/Get/?id=999";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_kasutaja()
        {
            // Arrange
            var url = "/api/Kasutajad/Delete/";

            var kasutaja = new Kasutaja
            {
                Eesnimi = "Mati",
                Perekonnanimi = "Tamm",
                Email = "mati@test.ee",
                Parool = "parool123"
            };
            await DbContext.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = kasutaja.Id })
            };
            using var response = await Client.SendAsync(request);
            var kasutajaFromDb = await DbContext.Kasutajad
                .Where(k => k.Id == kasutaja.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Null(kasutajaFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_shouldwork_with_missing_kasutaja()
        {
            // Arrange
            var url = "/api/Kasutajad/Delete/";

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
        public async Task Save_should_add_new_kasutaja()
        {
            // Arrange
            var url = "/api/Kasutajad/Save/";
            var command = new SaveKasutajaCommand
            {
                Eesnimi = "Mati",
                Perekonnanimi = "Tamm",
                Email = "mati@test.ee",
                Parool = "parool123"
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var kasutajaFromDb = await DbContext.Kasutajad
                .Where(k => k.Email == "mati@test.ee")
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(kasutajaFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_missing_kasutaja()
        {
            // Arrange
            var url = "/api/Kasutajad/Save/";
            var command = new SaveKasutajaCommand
            {
                Id = 999,
                Eesnimi = "Mati",
                Perekonnanimi = "Tamm",
                Email = "mati@test.ee",
                Parool = "parool123"
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var kasutajaFromDb = await DbContext.Kasutajad
                .Where(k => k.Id == 999)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(kasutajaFromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_invalid_kasutaja()
        {
            // Arrange
            var url = "/api/Kasutajad/Save/";
            var command = new SaveKasutajaCommand
            {
                Id = 0,
                Eesnimi = "",
                Perekonnanimi = "",
                Email = "",
                Parool = ""
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
