using CountriesProcessing.Models;

namespace CountriesProcessing.Helpers {
  public static class CountryHelpers {
    public static List<Country> FilterByName(List<Country> countries, string name) {
      return countries.Where(country =>
          country.Name != null &&
          country.Name.Common != null &&
          country.Name.Common.Contains(name, StringComparison.OrdinalIgnoreCase))
        .ToList();
    }

    public static List<Country> FilterByPopulation(List<Country> countries, int population) {
      return countries.Where(country => country.Population < population).ToList();
    }

    public static List<Country> SortCountries(List<Country> countries, string sortOrder) {
      if (sortOrder == "ascend") {
        return countries.OrderBy(c => c.Name.Common).ToList();
      }
      else if (sortOrder == "descend") {
        return countries.OrderByDescending(c => c.Name.Common).ToList();
      }
      else {
        throw new ArgumentException("Invalid sort order provided");
      }
    }

    public static List<Country> Pagination(List<Country> countries, int count) {
      return countries.Take(count).ToList();
    }

  }
}
