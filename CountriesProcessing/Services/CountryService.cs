using CountriesProcessing.Models;

namespace CountriesProcessing.Services {
  public class CountryService : ICountryService {
    public List<Country> FilterCountriesByName(List<Country> countries, string name) {
      return countries.Where(country =>
          country.Name != null &&
          country.Name.Common != null &&
          country.Name.Common.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
        .ToList();
    }
  }
}
