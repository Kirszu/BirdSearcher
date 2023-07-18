namespace BirdSearcher.Services;

using BirdSearcher.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
public class ObservationService
{
    private readonly HttpClient _httpClient;

    public ObservationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Observation>> GetRecentObservationsByRegionCode(string regionCode, string apiKey)
    {
        var response = await _httpClient.GetAsync($"https://api.ebird.org/v2/data/obs/{regionCode}/recent?key={apiKey}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

}

