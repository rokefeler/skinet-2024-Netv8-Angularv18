using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
  Expression<Func<T, bool>>? Criteria { get; } //optional
  Expression<Func<T, object>>? OrderBy { get; }
  Expression<Func<T, object>>? OrderByDescending { get; }
  bool IsDistinct { get; }
  // List<Expression<Func<T, object>>> Includes { get; }
  // List<string> IncludeStrings { get; }
}

/* Esto para que funcione Filtros por tipo que no devuelven el mismo objeto T*/

public interface ISpecification<T, TResult> : ISpecification<T>
{
  Expression<Func<T, TResult>>? Select { get; }
}