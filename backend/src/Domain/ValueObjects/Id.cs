namespace Domain.ValueObjects;

public class Id
{
    public string Value { get => this.value.ToString(); }
    private Guid value;

    public Id(string value) => this.value = new Guid(value);

    public static Id Generate() => new Id(Guid.NewGuid().ToString());
}