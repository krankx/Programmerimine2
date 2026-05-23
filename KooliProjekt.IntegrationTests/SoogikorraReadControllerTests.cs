using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class SoogikorraReadControllerTests : TestBase
    {
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
            var kasutaja = new Kasutaja
            {
                Eesnimi = "Kasutaja",
                Perekonnanimi = "Nimi",
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
            await DbContext.SaveChangesAsync();

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
    }
}
