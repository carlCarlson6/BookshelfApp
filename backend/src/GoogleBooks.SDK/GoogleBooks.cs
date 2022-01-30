using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace GoogleBooks.SDK;

public interface IGoogleBooks
{
    Task<GoogleBooksResponse?> Search(string isbn);
}

public class GoogleBooks : IGoogleBooks
{
    private readonly HttpClient _http;

    public GoogleBooks(GoogleBooksSettings configuration)
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri(configuration.BaseUrl);
    }

    public async Task<GoogleBooksResponse?> Search(string isbn)
    {
        var query = new Dictionary<string, string>
        {
            ["q"] = $"isbn:{isbn}"
        };

        var requestUri = QueryHelpers.AddQueryString("/api/", query);
        
        var response = await _http
            .GetFromJsonAsync<GoogleBooksResponse>(requestUri);

        if (response is null || response.TotalItems is 0)
            return null;

        return response;
    }
}
