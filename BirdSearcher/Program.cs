using BirdSearcher.Models;
using BirdSearcher.Services;
using Microsoft.Extensions.Configuration;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();



var httpClient = new HttpClient();
var observationService = new ObservationService(httpClient);

var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();

var apiKey = config.GetSection("eBirdApi:ApiKey").Value;
List<Observation> observations = await observationService.GetRecentObservationsByRegionCode("PL", apiKey);

foreach (var observation in observations)
{
    Console.WriteLine(observation.comName);
}

Console.ReadLine();


