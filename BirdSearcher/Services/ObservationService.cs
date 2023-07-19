namespace BirdSearcher.Services;

using BirdSearcher.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
public class ObservationService
{
    private readonly HttpClient _httpClient;
    private readonly string _eBirdApiToken = "X-eBirdApiToken";
    private readonly string _apiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("eBirdApi:ApiKey").Value;

    public ObservationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    private HttpRequestMessage GetRequestBuilder(string requestUrl)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
        request.Headers.Add(_eBirdApiToken, _apiKey);
        return request;
    }

    private async Task<string> GetResponseContent(HttpRequestMessage request)
    {
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    public async Task<List<Observation>> GetRecentObservationsByRegionCode(string regionCode)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/obs/{regionCode}/recent");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    public async Task<List<Observation>> GetRecentNotableObservationsByRegionCode(string regionCode)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/obs/{regionCode}/recent/notable?detail=full");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    public async Task<List<Observation>> GetRecentObservationsOfASpeciesByRegionCode(string regionCode, string speciesCode)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/obs/{regionCode}/recent/{speciesCode}");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    public async Task<List<Observation>> GetRecentNearbyObservations(double lat, double lng)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/obs/geo/recent?lat={lat}&lng={lng}&sort=species");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    public async Task<List<Observation>> GetRecentNearbyObservationsBySpecies(double lat, double lng, string speciesCode)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/obs/geo/recent/{speciesCode}?lat={lat}&lng={lng}");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    public async Task<List<Observation>> GetNearestObservationsBySpecies(double lat, double lng, string speciesCode)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/nearest/geo/recent/{speciesCode}?lat={lat}&lng={lng}");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    public async Task<List<Observation>> GetRecentNearbyNotableObservations(double lat, double lng)
    {
        var request = GetRequestBuilder($"https://api.ebird.org/v2/data/obs/geo/recent/notable?lat={lat}&lng={lng}");
        var responseContent = await GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

}

