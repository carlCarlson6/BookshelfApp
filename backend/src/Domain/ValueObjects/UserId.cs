namespace Domain.ValueObjects;

public class UserId : Id
{
    public UserId(string value) : 
        base(value) { }
    
    public static UserId Generate() => new (Guid.NewGuid().ToString());
}