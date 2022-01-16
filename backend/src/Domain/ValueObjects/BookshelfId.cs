namespace Domain.ValueObjects;

public class BookshelfId : Id
{
    public BookshelfId(string value) : 
        base(value) { }
    
    public static BookshelfId Generate() => new (Guid.NewGuid().ToString());
}