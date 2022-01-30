using System.Text.RegularExpressions;
using Domain.Exceptions;
using static System.String;

namespace Bookshelf.Bookshelfs;

public record Isbn
{
    private readonly string _isbn;
    
    public Isbn(string value)
    {
        if (IsNullOrEmpty(value) || IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        
        value = CleanIsbn(value);
        
        if (!ValidateIsbn(value))
            throw new InvalidIsbnException(value);

        _isbn = value;
    }

    private string CleanIsbn(string inputIsbn) => RemoveNonNumeric(inputIsbn);
    
    private static string RemoveNonNumeric(string value) => Regex.Replace(value, "[^0-9]", "");
    
    private static bool ValidateIsbn(string isbn)
    {
        var isOnlyNumber = Regex.IsMatch(isbn, "^[0-9]*$");
        var isLength10Or13 = isbn.Length is 10 or 13;
        return isOnlyNumber && isLength10Or13;
    }
}