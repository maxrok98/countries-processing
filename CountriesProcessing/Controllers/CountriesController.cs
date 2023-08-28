using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using CountriesProcessing.Models;

namespace CountriesProcessing.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class CountriesController : ControllerBase {
    private const string RestCountriesUrl = "https://restcountries.com/v3.1/all";

    private readonly IHttpClientFactory _httpClientFactory;

    public CountriesController(IHttpClientFactory httpClientFactory) {
      _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IEnumerable<Country>> GetCountries(string? name, int? population, string? sortBy, int? count) {
      using HttpClient client = _httpClientFactory.CreateClient();
      var response = await client.GetStringAsync(RestCountriesUrl);
      var options = new JsonSerializerOptions {
        PropertyNameCaseInsensitive = true
      };
      var countries = JsonSerializer.Deserialize<List<Country>>(response, options);

      return countries;
    }
  }
}