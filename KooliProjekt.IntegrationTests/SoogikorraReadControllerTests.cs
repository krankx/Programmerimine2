using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Features.SoogikorraRead;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class SoogikorraReadControllerTests : TestBase
    {
        private async Task<(Soogikord soogikord, Toiduaine toiduaine)> CreateContext()
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

            var soogikord = new Soogikord
            {
                Kuupaev = new DateTime(2025, 1, 1),
                Tyyp = SoogikorraTyyp.Louna,
                PatsientId = patsient.Id
            };
            await DbContext.AddAsync(soogikord);

            var toiduaine = new Toiduaine
            {
                Nimetus = "Leib",
                Energia = 250m
            };
            await DbContext.AddAsync(toiduaine);
            await DbContext.SaveChangesAsync();

            return (soogikord, toiduaine);
        }

        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/SoogikorraRead/List/?page=1&pageSize=10";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<SoogikorraRida>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_soogikorra_rida()
        {
            // Arrange
            var (soogikord, toiduaine) = await CreateContext();
            var rida = new SoogikorraRida
            {
                Kogus = 100m,
                SoogikordId = soogikord.Id,
                ToiduaineId = toiduaine.Id
            };
            await DbContext.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            var url = "/api/SoogikorraRead/Get/?id=" + rida.Id;

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<SoogikorraRidaDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_soogikorra_rida()
        {
            // Arrange
            var url = "/api/SoogikorraRead/Get/?id=999";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_should_remove_existing_soogikorra_rida()
        {
            // Arrange
            var url = "/api/SoogikorraRead/Delete/";
            var (soogikord, toiduaine) = await CreateContext();
            var rida = new SoogikorraRida
            {
                Kogus = 100m,
                SoogikordId = soogikord.Id,
                ToiduaineId = toiduaine.Id
            };
            await DbContext.AddAsync(rida);
            await DbContext.SaveChangesAsync();

            // Act
            using var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = rida.Id })
            };
            using var response = await Client.SendAsync(request);
            var fromDb = await DbContext.SoogikorraRead
                .Where(r => r.Id == rida.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Delete_shouldwork_with_missing_soogikorra_rida()
        {
            // Arrange
            var url = "/api/SoogikorraRead/Delete/";

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
        public async Task Save_should_add_new_soogikorra_rida()
        {
            // Arrange
            var url = "/api/SoogikorraRead/Save/";
            var (soogikord, toiduaine) = await CreateContext();
            var command = new SaveSoogikorraRidaCommand
            {
                Kogus = 100m,
                SoogikordId = soogikord.Id,
                ToiduaineId = toiduaine.Id
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.SoogikorraRead
                .Where(r => r.SoogikordId == soogikord.Id)
                .FirstOrDefaultAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_missing_soogikorra_rida()
        {
            // Arrange
            var url = "/api/SoogikorraRead/Save/";
            var (soogikord, toiduaine) = await CreateContext();
            var command = new SaveSoogikorraRidaCommand
            {
                Id = 999,
                Kogus = 100m,
                SoogikordId = soogikord.Id,
                ToiduaineId = toiduaine.Id
            };

            // Act
            using var response = await Client.PostAsJsonAsync(url, command);
            var fromDb = await DbContext.SoogikorraRead
                .Where(r => r.Id == 999)
                .FirstOrDefaultAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(fromDb);
            var result = await response.Content.ReadFromJsonAsync<OperationResult>();
            Assert.True(result.HasErrors);
        }

        [Fact]
        public async Task Save_should_work_with_invalid_soogikorra_rida()
        {
            // Arrange
            var url = "/api/SoogikorraRead/Save/";
            var command = new SaveSoogikorraRidaCommand
            {
                Id = 0,
                Kogus = 0m,
                SoogikordId = 0,
                ToiduaineId = 0
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
