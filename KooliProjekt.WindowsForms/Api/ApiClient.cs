using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms.Api
{
    public class ApiClient : IApiClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;

        public ApiClient()
        {
            _baseUrl = "http://localhost:5086/api/Toiduained/";
            _client = new HttpClient();
        }

        public async Task<OperationResult<PagedResult<Toiduaine>>> List(int page, int pageSize)
        {
            var url = _baseUrl + "List?page=" + page + "&pageSize=" + pageSize;

            return await _client.GetFromJsonAsync<OperationResult<PagedResult<Toiduaine>>>(url);
        }

        public async Task Save(Toiduaine toiduaine)
        {
            var url = _baseUrl + "Save";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(toiduaine)
            };

            await _client.SendAsync(request);
        }

        public async Task Delete(int id)
        {
            var url = _baseUrl + "Delete";

            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(new { id = id })
            };

            await _client.SendAsync(request);
        }
    }
}
