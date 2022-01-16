using System.Linq.Expressions;
using Domain.ValueObjects;

namespace Domain;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();
        
    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }
}

public class UserByEmailSpecification : Specification<Email>
{
    private readonly Email _email;

    public UserByEmailSpecification(Email email)
    {
        _email = email;
    }

    public override Expression<Func<Email, bool>> ToExpression() => email => _email.InputIsbn.Equals(email.InputIsbn);
}