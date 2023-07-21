using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using ReactApp.Models;
using ReactApp.Controllers;
using ReactApp.APICallHelpers;

public class ObservationControllerTests
{
    [Fact]
    public async Task GetRecentObservationsByRegionCode_Should_Return_Observations()
    {
        // Arrange
        var expectedObservations = new List<Observation>
        {
            new Observation { comName = "Robin", obsDt = DateTime.Now },
            new Observation { comName = "Sparrow", obsDt = DateTime.Now }
        };

        //Mock HTTP client
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { 
                StatusCode = HttpStatusCode.OK, 
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(expectedObservations))
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);
        HttpService _httpService = new(client);

        var observationService = new ObservationController(_httpService);

        // Act
        var result = await observationService.GetRecentObservationsByRegionCode("PL");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedObservations.Count, result.Count);
    }
}