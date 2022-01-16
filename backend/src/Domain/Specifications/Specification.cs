using System.Linq.Expressions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();
    public abstract Predicate<T> ToPredicate();

    public bool IsSatisfiedBy(T entity) => ToExpression().Compile()(entity);
}
