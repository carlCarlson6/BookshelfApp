namespace Domain.ValueObjects;

public abstract class BookshelfId : Id
{
    public BookshelfId(string value) : 
        base(value) { }
}