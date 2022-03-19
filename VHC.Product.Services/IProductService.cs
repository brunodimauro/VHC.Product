namespace VHC.Product.Services
{
    public interface IProductService
    {
        Task<Domain.Product?> Get(Guid id);
        Task<List<Domain.Product>?> Get();
        Task<List<Domain.Product>?> ListByFilter(Domain.ProductFilter filter);
        Task Insert(Domain.Product entity);
        Task Update(Domain.Product entity);
        Task Delete(Guid id);
        Task InsertList(IEnumerable<Domain.Product> list);
    }
}