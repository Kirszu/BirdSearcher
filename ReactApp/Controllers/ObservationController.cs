namespace ReactApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using ReactApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using APICallHelpers;

[ApiController]
[Route("[controller]")] 
public class ObservationController : ControllerBase
{
    public HttpService _httpService = new(new HttpClient());

    public ObservationController(HttpService httpService)
    {
        _httpService = httpService;
    }

    [HttpGet("{regionCode}/recent")]
    public async Task<List<Observation>> GetRecentObservationsByRegionCode(string regionCode)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/obs/{regionCode}/recent");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    [HttpGet]
    public async Task<List<Observation>> GetRecentNotableObservationsByRegionCode(string regionCode)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/obs/{regionCode}/recent/notable?detail=full");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    [HttpGet]
    public async Task<List<Observation>> GetRecentObservationsOfASpeciesByRegionCode(string regionCode, string speciesCode)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/obs/{regionCode}/recent/{speciesCode}");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    [HttpGet]
    public async Task<List<Observation>> GetRecentNearbyObservations(double lat, double lng)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/obs/geo/recent?lat={lat}&lng={lng}&sort=species");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    [HttpGet]
    public async Task<List<Observation>> GetRecentNearbyObservationsBySpecies(double lat, double lng, string speciesCode)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/obs/geo/recent/{speciesCode}?lat={lat}&lng={lng}");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    [HttpGet]
    public async Task<List<Observation>> GetNearestObservationsBySpecies(double lat, double lng, string speciesCode)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/nearest/geo/recent/{speciesCode}?lat={lat}&lng={lng}");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

    [HttpGet]
    public async Task<List<Observation>> GetRecentNearbyNotableObservations(double lat, double lng)
    {
        var request = _httpService.GetRequestBuilder($"https://api.ebird.org/v2/data/obs/geo/recent/notable?lat={lat}&lng={lng}");
        var responseContent = await _httpService.GetResponseContent(request);

        var observations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Observation>>(responseContent);

        return observations;
    }

}

