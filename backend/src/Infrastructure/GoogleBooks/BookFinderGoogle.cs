using System.Net.Http.Json;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;
using Microsoft.AspNetCore.WebUtilities;

namespace Infrastructure.GoogleBooks;

public class BookFinderGoogle : IBookFinder
{
    private readonly HttpClient _http;

    public BookFinderGoogle(GoogleBooksApiConfiguration configuration)
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri(configuration.BaseUrl);
    }

    public async Task<Book?> Search(Isbn isbn)
    {
        var query = new Dictionary<string, string>
        {
            ["q"] = $"isbn:{isbn}"
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
