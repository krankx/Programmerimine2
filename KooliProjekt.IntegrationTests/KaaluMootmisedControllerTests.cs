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
    public class KaaluMootmisedControllerTests : TestBase
    {
        [Fact]
        public async Task List_should_return_paged_result()
        {
            // Arrange
            var url = "/api/KaaluMootmised/List/?page=1&pageSize=10";

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<PagedResult<KaaluMootmine>>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_kaalumootmine()
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

            var mootmine = new KaaluMootmine
            {
                Kuupaev = new DateTime(2025, 1, 1),
                Kaal = 80m,
                PatsientId = patsient.Id
            };
            await DbContext.AddAsync(mootmine);
            await DbContext.SaveChangesAsync();

            var url = "/api/KaaluMootmised/Get/?id=" + mootmine.Id;

            // Act
            var response = await Client.GetFromJsonAsync<OperationResult<KaaluMootmineDto>>(url);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.HasErrors);
        }

        [Fact]
        public async Task Get_should_return_not_found_for_missing_kaalumootmine()
        {
            // Arrange
            var url = "/api/KaaluMootmised/Get/?id=999";

            // Act
            var response = await Client.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
