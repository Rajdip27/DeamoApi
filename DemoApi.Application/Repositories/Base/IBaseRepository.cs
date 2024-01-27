using DemoApi.Domain.Extensions.Dropdown;
using DemoApi.Domain.Extensions.Pagging;
using DemoApi.Domain.Model.BaseEntities;
using System.Linq.Expressions;

namespace DemoApi.Application.Repositories.Base;

public interface IBaseRepository<TEntity, TModel,T>
     where TEntity : AuditableEntity, new()
    where T : IEquatable<T>
{
    #region GetPageAsync

    Task<Paging<TModel>> GetPageAsync(int pageIndex, int pageSize);
    Task<Paging<TModel>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
    Task<Paging<TModel>> GetPageAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes);
    Task<Paging<TModel>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes);
    Task<Paging<TResult>> GetPageAsync<TResult>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, object>>[] includes);

    #endregion
    #region GetDropdownAsync
    Task<Dropdown<TModel>> GetDropdownAsync(Expression<Func<TEntity, bool>> predicate, int size);
    Task<Dropdown<TResult>> GetDropdownAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Expression<Func<TEntity, TResult>> selector, int size);
    #endregion
    #region CRUDOperation

    Task<TModel> InsertAsync(TEntity entity);
    Task<TModel> UpdateAsync(T id, TEntity entity);
    Task<TModel> DeleteAsync(T id);

    Task<TModel> InsertRangeAsync(List<TEntity> entities);
    Task<TModel> UpdateRangeAsync(List<TEntity> entities);
    Task<TModel> DeleteRangeAsync(List<TEntity> entities);

    #endregion


    #region FirstOrDefault
    Task<TModel> FirstOrDefaultAsync(T id);
    Task<TModel> GetAsNoTrackingAsync(T id);
    Task<TModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TModel> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
    Task<TModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<TModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
    Task<TModel> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes);

    #endregion

    #region GetAllAsync

    Task<List<TModel>> GetAllAsync();
    Task<List<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);
    Task<List<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
    Task<List<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes);

    #endregion

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
    void Attach(TEntity entity);
    void Detach(TEntity entity);
}
