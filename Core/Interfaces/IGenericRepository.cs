using Core.Entities;

namespace Core.Interfaces;

  public interface IGenericRepository<T> where T : BaseEntity<int>
  {
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    /* Para que devuelva diferentes tipos de datos */
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);

    /* CRUD */
    void Add(T entity); 
    void Update(T entity);
    void Remove(T entity);
    Task<bool> SaveChangesAsync();
    bool Exists(int id);
  }