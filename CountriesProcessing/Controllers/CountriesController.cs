using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace CountriesProcessing.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class CountriesController : ControllerBase {
    private const string Url = "https://restcountries.com/v3.1/all";

    private readonly IHttpClientFactory _httpClientFactory;

    public CountriesController(IHttpClientFactory httpClientFactory) {
      _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IEnumerable<Country>> GetCountries(string? name, int? population, string? sortBy, int? count) {
      using HttpClient client = _httpClientFactory.CreateClient();
      var response = await client.GetStringAsync(Url);
      var options = new JsonSerializerOptions {
        PropertyNameCaseInsensitive = true
      };
      var countries = JsonSerializer.Deserialize<List<Country>>(response, options);

      return countries;
    }
  }
}