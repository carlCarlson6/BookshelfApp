namespace Domain.ValueObjects;

public class Email : StringValueObject
{
    public Email(string inputIsbn) : base(inputIsbn) { }
}