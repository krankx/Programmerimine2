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
    }
}
