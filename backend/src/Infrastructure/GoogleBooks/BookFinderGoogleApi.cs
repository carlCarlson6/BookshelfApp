using System.Net.Http.Json;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.GoogleBooks;

public class BookFinderGoogleApi : IBookFinderApi
{
    private readonly GoogleBooksApiConfiguration _configuration;
    private readonly HttpClient _http;

    public BookFinderGoogleApi(GoogleBooksApiConfiguration configuration)
    {
        _configuration = configuration;
        _http = new HttpClient();
        _http.BaseAddress = new Uri(_configuration.BaseUrl);
    }

    public async Task<Book?> Search(Isbn isbn)
    {
        var query = new Dictionary<string, string>
        {
            ["q"] = $"isbn:{isbn}",
            ["key"] = _configuration.ApiKey,
        };

        var response = await _http
            .GetFromJsonAsync<GoogleBooksResponse>(QueryHelpers.AddQueryString("/api/", query));

        if (response is null || response.TotalItems is 0)
            return null;

        var bookItem = response.Items.First();
        return new Book(
            isbn,
            bookItem.VolumeInfo.Title,
            string.Join(", ", bookItem.VolumeInfo.Authors),
            string.Empty,
            bookItem.VolumeInfo.Publisher,
            Convert.ToDateTime(bookItem.VolumeInfo.PublishedDate),
            bookItem.VolumeInfo.Description,
            (uint)bookItem.VolumeInfo.PageCount,
            bookItem.VolumeInfo.ImageLinks.Thumbnail,
            bookItem.VolumeInfo.ImageLinks.SmallThumbnail);
    }
}
