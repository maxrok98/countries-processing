using CountriesProcessing.Models;

namespace CountriesProcessing.Services {
  public interface ICountryService {
    List<Country> FilterCountriesByName(List<Country> countries, string name);
  }
}
