using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecifications : BaseSpecification<Product, string>
{
    public BrandListSpecifications()
    {
      AddSelect(x => x.Brand);
      ApplyDistinct();
    }
}
