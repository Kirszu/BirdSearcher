using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using BirdSearcher.Models;
using BirdSearcher.Services;

public class ObservationServiceTests
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

        var observationService = new ObservationService(client);

        // Act
        var result = await observationService.GetRecentObservationsByRegionCode("PL");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedObservations.Count, result.Count);
    }
}