using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Isbn : StringValueObject
{
    public Isbn(string value) : base(value)
    {
        value = CleanIsbn(value);
        
        if (!ValidateIsbn(value))
            throw new InvalidIsbnException(value);
    }

    private string CleanIsbn(string inputIsbn) => RemoveNonNumeric(inputIsbn);
    private static string RemoveNonNumeric(string value) => Regex.Replace(value, "[^0-9]", "");
    
    private static bool ValidateIsbn(string isbn)
    {
        var isOnlyNumber = Regex.IsMatch(isbn, "^[0-9]*$");
        var isLength10Or13 = isbn.Length is 10 or 13;
        return isOnlyNumber && isLength10Or13;
    }
    
    public bool Equals(Isbn isbn) => isbn.Value == Value;
}