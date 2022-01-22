using System.Linq.Expressions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Specifications;

public class UserByEmailSpecification : Specification<User>
{
    private readonly Email _email;

    public UserByEmailSpecification(Email email) => _email = email;

    public override Expression<Func<User, bool>> ToExpression() => user => _email.ToString() == user.Email.ToString();
    
    public override Predicate<User> ToPredicate() => user => _email.ToString() == user.Email.ToString();
}