using Core.Entities;
using Core.Interfaces;

namespace Infrastructure;

public class SpecificationEvaluator<T> where T : BaseEntity<int>
{
  public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
  {
    if(spec.Criteria is not null)
    {
      query = query.Where(spec.Criteria); //where clause
    }

    /*** la variable query es un objeto IQueryable<T>,
    por lo que, NO ES UNA COLECCION que se pueda modificar mediante += 
    ***/

    //ordenacion se aplica sobre consulta de resultados 
    if(spec.OrderBy is not null)
    {
      query = query.OrderBy(spec.OrderBy);
    }

    if(spec.OrderByDescending is not null)
    {
      query = query.OrderByDescending(spec.OrderByDescending);
    }

    if(spec.IsDistinct)
    {
      query = query.Distinct();
    }

    return query;
  }

  public static IQueryable<TResult> GetQuery<TSpec, TResult>
      (IQueryable<T> query, ISpecification<T, TResult> spec)
  {
    if(spec.Criteria is not null)
    {
      query = query.Where(spec.Criteria); //where clause
    }

    if(spec.OrderBy is not null)
    {
      query = query.OrderBy(spec.OrderBy);
    }

    if(spec.OrderByDescending is not null)
    {
      query = query.OrderByDescending(spec.OrderByDescending);
    }
    var selectQuery = query as IQueryable<TResult>;
    if(spec.Select is not null)
    {
      selectQuery = query.Select(spec.Select);
    }
    if(spec.IsDistinct)
    {
      selectQuery = selectQuery?.Distinct();
    }

    return selectQuery ?? query.Cast<TResult>();
  }
}
