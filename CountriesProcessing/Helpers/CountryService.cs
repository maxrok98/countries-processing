using CountriesProcessing.Models;

namespace CountriesProcessing.Helpers {
  public static class CountryHelpers {
    public static List<Country> FilterByName(List<Country> countries, string name) {
      return countries.Where(country =>
          country.Name != null &&
          country.Name.Common != null &&
          country.Name.Common.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
        .ToList();
    }

    public static List<Country> FilterByPopulation(List<Country> countries, int population) {
      return countries.Where(country => country.Population < population).ToList();
    }
  }
}
