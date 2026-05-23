using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Features.Patsiendid;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class PatsiendidControllerTests : TestBase
    {
        private async Task<Kasutaja> CreateKasutaja()
        {
            var kasutaja = new Kasutaja
            {
                Eesnimi = "Kasutaja",
                Perekonnanimi = "Nimi",
                Email = "k@test.ee",
                Parool = "parool"
            };
            await DbContext.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();
            return kasutaja;
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/Patsiendid/List/?page=1&pageSize=10";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<Patsient>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_patsient()
        {
            // Arrange
            var kasutaja = await CreateKasutaja();
            var patsient = new Patsient
            {
                Eesnimi = "Jaan",
                Perekonnanimi = "Tamm",
                Isikukood = "12345678901",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = kasutaja.Id
            };
            await DbContext.AddAsync(patsient);
            await DbContext.SaveChangesAsync();

            var url = "/api/Patsiendid/Get/?id=" + patsient.Id;

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PatsientDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_patsient()
        {
            // Arrange
            var url = "/api/Patsiendid/Get/?id=999";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_patsient()
        {
            // Arrange
            var url = "/api/Patsiendid/Delete/";
            var kasutaja = await CreateKasutaja();
            var patsient = new Patsient
            {
                Eesnimi = "Jaan",
                Perekonnanimi = "Tamm",
                Isikukood = "12345678901",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = kasutaja.Id
            };
            await DbContext.AddAsync(patsient);
            await DbContext.SaveChangesAsync();

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = patsient.Id })
            };
            using var response = await Client.SendAsync(request);
            var fromDb = await DbContext.Patsiendid
                .Where(p => p.Id == patsient.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_shouldwork_with_missing_patsient()
        {
            // Arrange
            var url = "/api/Patsiendid/Delete/";

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
        public async Task Save_should_add_new_patsient()
        {
            // Arrange
            var url = "/api/Patsiendid/Save/";
            var kasutaja = await CreateKasutaja();
            var command = new SavePatsientCommand
            {
                Eesnimi = "Jaan",
                Perekonnanimi = "Tamm",
                Isikukood = "12345678901",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = kasutaja.Id
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.Patsiendid
                .Where(p => p.Isikukood == "12345678901")
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_missing_patsient()
        {
            // Arrange
            var url = "/api/Patsiendid/Save/";
            var kasutaja = await CreateKasutaja();
            var command = new SavePatsientCommand
            {
                Id = 999,
                Eesnimi = "Jaan",
                Perekonnanimi = "Tamm",
                Isikukood = "12345678901",
                Synniaeg = new DateTime(1990, 1, 1),
                KasutajaId = kasutaja.Id
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.Patsiendid
                .Where(p => p.Id == 999)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_invalid_patsient()
        {
            // Arrange
            var url = "/api/Patsiendid/Save/";
            var command = new SavePatsientCommand
            {
                Id = 0,
                Eesnimi = "",
                Perekonnanimi = "",
                Isikukood = "",
                KasutajaId = 0
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
