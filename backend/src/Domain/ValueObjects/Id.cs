namespace Domain.ValueObjects;

public class Id
{
    private string Value { get => _value.ToString(); }
    private readonly Guid _value;

    protected Id(string value) => _value = new Guid(value);
    
    public override string ToString() => Value;
}