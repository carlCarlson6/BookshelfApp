namespace Domain.ValueObjects;

public class Id
{
    public string Value { get => _value.ToString(); }
    private readonly Guid _value;

    public Id(string value) => _value = new Guid(value);
    
    public override string ToString() => Value;
}