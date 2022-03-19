using VHC.Product.Helpers.Domain;

namespace VHC.Product.Helpers.Repository
{
    public interface IRepository<TEntity, TFilter>
        where TEntity : IDomain
        where TFilter : IFilter
    {
        Task<TEntity?> Get(Guid id);
        Task<List<TEntity>?> Get();
        Task<List<TEntity>?> ListByFilter(TFilter filter);
        Task Insert(TEntity entity);
        Task InsertList(IEnumerable<TEntity> entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
