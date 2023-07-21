namespace ReactApp.APICallHelpers
{
public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _eBirdApiToken = "X-eBirdApiToken";
        private readonly string _apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("eBirdApi:ApiKey").Value;

        public HttpService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public HttpRequestMessage GetRequestBuilder(string requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add(_eBirdApiToken, _apiKey);
            return request;
        }
        public async Task<string> GetResponseContent(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
