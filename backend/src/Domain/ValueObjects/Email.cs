namespace Domain.ValueObjects;

public class Email : StringValueObject
{
    // TODO add validations
    public Email(string value) : base(value) { }

    public static Email Create(string inputEmail) => new Email(inputEmail);
}