using CountriesProcessing.Models;
using CountriesProcessing.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using Xunit;

namespace CountriesProcessing.Tests {
  public class CountryFilterTests {
    [Fact]
    public void FilterByName_WithMatchingName_ReturnsCorrectCountries() {
      // Arrange
      var countries = new List<Country> 
      {
        new Country { Name = new NameInfo { Common = "United States" } },
        new Country { Name = new NameInfo { Common = "United Kingdom" } },
        new Country { Name = new NameInfo { Common = "India" } }
      };
      string searchTerm = "United";

      // Act
      var result = CountryHelpers.FilterByName(countries, searchTerm);

      // Assert
      Assert.Equal(2, result.Count);
      Assert.True(result.Any(country => country.Name.Common == "United States"));
      Assert.True(result.Any(country => country.Name.Common == "United Kingdom"));
    }

    [Fact]
    public void FilterByName_WithNoMatchingName_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = new NameInfo { Common = "France" } },
        new Country { Name = new NameInfo { Common = "Germany" } }
      };
      string searchTerm = "United";

      // Act
      var result = CountryHelpers.FilterByName(countries, searchTerm);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void FilterByName_WithNullNameProperty_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = null }
      };
      string searchTerm = "United";

      // Act
      var result = CountryHelpers.FilterByName(countries, searchTerm);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void FilterByName_WithNullCommonNameProperty_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = new NameInfo { Common = null } }
      };
      string searchTerm = "United";

      // Act
      var result = CountryHelpers.FilterByName(countries, searchTerm);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void FilterByPopulation_WithCountriesBelowThreshold_ReturnsFilteredCountries() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Population = 5_000_000 },
        new Country { Population = 10_000_000 },
        new Country { Population = 15_000_000 }
      };
      int populationThreshold = 12_000_000;

      // Act
      var result = CountryHelpers.FilterByPopulation(countries, populationThreshold);

      // Assert
      Assert.Equal(2, result.Count);
      Assert.Contains(result, country => country.Population == 5_000_000);
      Assert.Contains(result, country => country.Population == 10_000_000);
    }

    [Fact]
    public void FilterByPopulation_WithNoCountriesBelowThreshold_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Population = 20_000_000 },
        new Country { Population = 25_000_000 }
      };
      int populationThreshold = 15_000_000;

      // Act
      var result = CountryHelpers.FilterByPopulation(countries, populationThreshold);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void FilterByPopulation_WithEmptyCountryList_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>();
      int populationThreshold = 15_000_000;

      // Act
      var result = CountryHelpers.FilterByPopulation(countries, populationThreshold);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void SortCountries_AscendingOrder_ReturnsCountriesInAscendingOrder() {
      // Arrange
      var countries = new List<Country>
      {
            new Country { Name = new NameInfo { Common = "India" } },
            new Country { Name = new NameInfo { Common = "France" } },
            new Country { Name = new NameInfo { Common = "Germany" } }
        };

      // Act
      var result = CountryHelpers.SortCountries(countries, "ascend");

      // Assert
      Assert.Equal("France", result[0].Name.Common);
      Assert.Equal("Germany", result[1].Name.Common);
      Assert.Equal("India", result[2].Name.Common);
    }

    [Fact]
    public void SortCountries_DescendingOrder_ReturnsCountriesInDescendingOrder() {
      // Arrange
      var countries = new List<Country>
      {
            new Country { Name = new NameInfo { Common = "India" } },
            new Country { Name = new NameInfo { Common = "France" } },
            new Country { Name = new NameInfo { Common = "Germany" } }
        };

      // Act
      var result = CountryHelpers.SortCountries(countries, "descend");

      // Assert
      Assert.Equal("India", result[0].Name.Common);
      Assert.Equal("Germany", result[1].Name.Common);
      Assert.Equal("France", result[2].Name.Common);
    }

    [Fact]
    public void SortCountries_InvalidOrder_ThrowsArgumentException() {
      // Arrange
      var countries = new List<Country>
      {
            new Country { Name = new NameInfo { Common = "India" } }
        };

      // Act & Assert
      Assert.Throws<ArgumentException>(() => CountryHelpers.SortCountries(countries, "invalidOrder"));
    }

    [Fact]
    public void SortCountries_EmptyList_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>();

      // Act
      var resultAscend = CountryHelpers.SortCountries(countries, "ascend");
      var resultDescend = CountryHelpers.SortCountries(countries, "descend");

      // Assert
      Assert.Empty(resultAscend);
      Assert.Empty(resultDescend);
    }

    [Fact]
    public void Pagination_StandardCase_ReturnsCorrectNumberOfCountries() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = new NameInfo { Common = "India" } },
        new Country { Name = new NameInfo { Common = "France" } },
        new Country { Name = new NameInfo { Common = "Germany" } }
      };
      int count = 2;

      // Act
      var result = CountryHelpers.Pagination(countries, count);

      // Assert
      Assert.Equal(count, result.Count);
      Assert.Equal("India", result[0].Name.Common);
      Assert.Equal("France", result[1].Name.Common);
    }

    [Fact]
    public void Pagination_CountGreaterThanListSize_ReturnsAllCountries() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = new NameInfo { Common = "India" } },
        new Country { Name = new NameInfo { Common = "France" } },
      };
      int count = 5;

      // Act
      var result = CountryHelpers.Pagination(countries, count);

      // Assert
      Assert.Equal(2, result.Count);
      Assert.Equal("India", result[0].Name.Common);
      Assert.Equal("France", result[1].Name.Common);
    }

    [Fact]
    public void Pagination_NegativeCount_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = new NameInfo { Common = "India" } },
        new Country { Name = new NameInfo { Common = "France" } },
      };
      int count = -2;

      // Act
      var result = CountryHelpers.Pagination(countries, count);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void Pagination_CountIsZero_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>
      {
        new Country { Name = new NameInfo { Common = "India" } },
        new Country { Name = new NameInfo { Common = "France" } },
      };
      int count = 0;

      // Act
      var result = CountryHelpers.Pagination(countries, count);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void Pagination_EmptyCountryList_ReturnsEmptyList() {
      // Arrange
      var countries = new List<Country>();
      int count = 3;

      // Act
      var result = CountryHelpers.Pagination(countries, count);

      // Assert
      Assert.Empty(result);
    }

    [Fact]
    public void Pagination_NullCountryList_ThrowsArgumentNullException() {
      // Arrange
      List<Country> countries = null;
      int count = 3;

      // Act & Assert
      Assert.Throws<ArgumentNullException>(() => CountryHelpers.Pagination(countries, count));
    }
  }

}
