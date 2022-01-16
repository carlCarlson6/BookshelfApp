using static System.String;

namespace Domain.ValueObjects;

public class StringValueObject
{
    public string InputIsbn { get; }
    
    public StringValueObject(string inputIsbn)
    {
        if (IsNullOrEmpty(inputIsbn) || IsNullOrWhiteSpace(inputIsbn))
            throw new ArgumentNullException(nameof(inputIsbn));

        InputIsbn = inputIsbn;
    }

    public override string ToString() => InputIsbn;
}