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
    }
}
