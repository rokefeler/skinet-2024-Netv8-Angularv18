using System.Dynamic;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    // Constructors default to null
    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, bool>>? Criteria => criteria;

    //Propiedades que definieran definir los criterios de Ordenacion para los datos.
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct {get; private set;}

    //Propiedades que solo seran accesibles a traves de la Clase base o Clases derivadas
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) 
      => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) 
      => OrderByDescending = orderByDescendingExpression;

    protected void ApplyDistinct() => IsDistinct = true;
}


public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
  : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    //Aqui usamos operador de afirmaciuon de no nulidad !, es decir ponemos null!
    protected BaseSpecification() : this(null!) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
      Select = selectExpression;
    }
}