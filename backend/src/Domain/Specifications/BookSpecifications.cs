using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specifications;

public class BooksByTitleSpecification : Specification<Book>
{
    private readonly string _title;
    
    public BooksByTitleSpecification(string title) => _title = title;
    
    public override Expression<Func<Book, bool>> ToExpression() => book => book.Title.Contains(_title);

    public override Predicate<Book> ToPredicate() => book => book.Title.Contains(_title);
}

public class BooksByAuthorSpecification : Specification<Book>
{
    private readonly string _author;
    
    public BooksByAuthorSpecification(string author) => _author = author;
    
    public override Expression<Func<Book, bool>> ToExpression() => book => book.Title.Contains(_author);

    public override Predicate<Book> ToPredicate() => book => book.Title.Contains(_author);
}

public class BooksByLocationSpecification : Specification<Book>
{
    private readonly string _location;
    
    public BooksByLocationSpecification(string location) => _location = location;
    
    public override Expression<Func<Book, bool>> ToExpression() => book => book.Title.Contains(_location);

    public override Predicate<Book> ToPredicate() => book => book.Title.Contains(_location);
}