using System.Linq.Expressions;
using Domain.ValueObjects;

namespace Domain.Specifications;

public class UserByEmailSpecification : Specification<Email>
{
    private readonly Email _email;

    public UserByEmailSpecification(Email email) => _email = email;

    public override Expression<Func<Email, bool>> ToExpression() => email => _email.Value.Equals(email.Value);
    
    public override Predicate<Email> ToPredicate() => throw new NotImplementedException();
}