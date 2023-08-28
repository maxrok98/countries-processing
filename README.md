## Countries processing


### Description

1. **Namespace & Dependencies**: The code consists of three namespaces: `CountriesProcessing.Controllers`, `CountriesProcessing.Helpers` and `CountriesProcessing.Models`.
2. **API Controller**: The `CountriesController` class within the Controllers namespace is an API controller, defined using the `ApiController` and `Route` attributes.
3. **Dependency Injection**: It utilizes the `IHttpClientFactory` for creating instances of `HttpClient`. This dependency is injected through its constructor.
4. **Endpoint Definition**: A single `[HttpGet]` endpoint called `GetCountries` is available which can fetch a list of countries. It can filter, sort, and paginate the results.
5. **Data Retrieval**: Data is fetched from "https://restcountries.com/v3.1/all". The response is deserialized into a list of `Country` objects using the `JsonSerializer`.
6. **Filter & Sort Options**: Filtering can be done by `name` and `population`. Sorting is allowed based on the country's name in ascending or descending order.
7. **Pagination**: An optional `count` parameter allows pagination by taking the specified number of countries.
8. **Helpers**: The `CountryHelpers` static class provides utility functions to filter, sort, and paginate the list of countries.
9. **Filter By Name**: Countries can be filtered by checking if their name contains a specific string (case-insensitive).
10. **Exception Handling**: Invalid sorting parameters will throw an `ArgumentException` to indicate that an unsupported sort order has been provided.

### Building
In order to build and run application in shell go to directory `CountriesProcessing` and in bash run `dotnet run`.

### Examples
Given the described endpoint in the `CountriesController`, below are 10 example usage scenarios:

1. **Retrieve All Countries**:
   - **Request**: `GET /Countries`
   - This will return a list of all countries without any filters.

2. **Retrieve Countries by Name**:
   - **Request**: `GET /Countries?name=Canada`
   - This will return countries with names containing "Canada".

3. **Retrieve Countries with a Population Less Than a Specific Value**:
   - **Request**: `GET /Countries?population=1000000`
   - This will list countries with a population of less than 1 million.

4. **Retrieve and Sort Countries in Ascending Order**:
   - **Request**: `GET /Countries?sortByNameOrder=ascend`
   - Countries will be sorted by their names in ascending order.

5. **Retrieve and Sort Countries in Descending Order**:
   - **Request**: `GET /Countries?sortByNameOrder=descend`
   - Countries will be sorted by their names in descending order.

6. **Pagination - Retrieve First 5 Countries**:
   - **Request**: `GET /Countries?count=5`
   - This will list the first 5 countries based on the default order.

7. **Retrieve Countries by Name & Sort in Ascending Order**:
   - **Request**: `GET /Countries?name=Ind&sortByNameOrder=ascend`
   - This will return countries with names containing "Ind", sorted in ascending order by their names.

8. **Retrieve Countries by Population and Sort in Descending Order**:
   - **Request**: `GET /Countries?population=5000000&sortByNameOrder=descend`
   - This will list countries with populations of less than 5 million, sorted in descending order by their names.

9. **Retrieve Countries by Name, Sort in Ascending Order & Paginate**:
   - **Request**: `GET /Countries?name=Aus&sortByNameOrder=ascend&count=3`
   - This will return countries with names containing "Aus", sorted in ascending order, and limited to the first 3 results.

10. **Pagination - Retrieve the Next 5 Countries After the First 5**:
   - **Request**: `GET /Countries?count=5&page=2` (Note: The provided endpoint does not support pagination through a 'page' parameter, but this is a common practice in APIs. To implement this functionality, the endpoint would need adjustments.)
   - This would list countries 6 through 10 based on the default order.

