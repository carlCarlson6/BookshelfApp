using static System.String;

namespace Domain.ValueObjects;

public class StringValueObject
{
    public string Value { get; }
    
    public StringValueObject(string value)
    {
        if (IsNullOrEmpty(value) || IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
}