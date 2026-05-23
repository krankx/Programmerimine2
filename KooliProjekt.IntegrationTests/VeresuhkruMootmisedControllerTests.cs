using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Features.VeresuhkruMootmised;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class VeresuhkruMootmisedControllerTests : TestBase
    {
        private async Task<Patsient> CreatePatsient()
        {
            var kasutaja = new Kasutaja
            {
                Eesnimi = "K",
                Perekonnanimi = "N",
                Email = "k@test.ee",
                Parool = "parool"
            };
            await DbContext.AddAsync(kasutaja);
            await DbContext.SaveChangesAsync();

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
            return patsient;
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/List/?page=1&pageSize=10";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<VeresuhkruMootmine>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_veresuhkrumootmine()
        {
            // Arrange
            var patsient = await CreatePatsient();
            var mootmine = new VeresuhkruMootmine
            {
                Kuupaev = new DateTime(2025, 1, 1),
                Kellaaeg = new TimeSpan(8, 30, 0),
                Veresuhkur = 5.5m,
                PatsientId = patsient.Id
            };
            await DbContext.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var url = "/api/VeresuhkruMootmised/Get/?id=" + mootmine.Id;

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<VeresuhkruMootmineDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_veresuhkrumootmine()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/Get/?id=999";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_veresuhkrumootmine()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/Delete/";
            var patsient = await CreatePatsient();
            var mootmine = new VeresuhkruMootmine
            {
                Kuupaev = new DateTime(2025, 1, 1),
                Kellaaeg = new TimeSpan(8, 30, 0),
                Veresuhkur = 5.5m,
                PatsientId = patsient.Id
            };
            await DbContext.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = mootmine.Id })
            };
            using var response = await Client.SendAsync(request);
            var fromDb = await DbContext.VeresuhkruMootmised
                .Where(v => v.Id == mootmine.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_shouldwork_with_missing_veresuhkrumootmine()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/Delete/";

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
        public async Task Save_should_add_new_veresuhkrumootmine()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/Save/";
            var patsient = await CreatePatsient();
            var command = new SaveVeresuhkruMootmineCommand
            {
                Kuupaev = new DateTime(2025, 1, 1),
                Kellaaeg = new TimeSpan(8, 30, 0),
                Veresuhkur = 5.5m,
                PatsientId = patsient.Id
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.VeresuhkruMootmised
                .Where(v => v.PatsientId == patsient.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_missing_veresuhkrumootmine()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/Save/";
            var patsient = await CreatePatsient();
            var command = new SaveVeresuhkruMootmineCommand
            {
                Id = 999,
                Kuupaev = new DateTime(2025, 1, 1),
                Kellaaeg = new TimeSpan(8, 30, 0),
                Veresuhkur = 5.5m,
                PatsientId = patsient.Id
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.VeresuhkruMootmised
                .Where(v => v.Id == 999)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_invalid_veresuhkrumootmine()
        {
            // Arrange
            var url = "/api/VeresuhkruMootmised/Save/";
            var command = new SaveVeresuhkruMootmineCommand
            {
                Id = 0,
                Veresuhkur = 0m,
                PatsientId = 0
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
