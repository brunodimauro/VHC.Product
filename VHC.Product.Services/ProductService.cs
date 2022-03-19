using VHC.Product.Domain;
using VHC.Product.Helpers.Repository;

namespace VHC.Product.Services
{
    public class ProductService : IProductService
    {
        IRepository<Domain.Product, Domain.ProductFilter> _productRepository;

        public ProductService(IRepository<Domain.Product, Domain.ProductFilter> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Delete(Guid id)
        {
            Domain.Product? product = await _productRepository.Get(id);
            if (product == null)
                throw new ApplicationException("Product not found");

            await _productRepository.Delete(product);
        }

        public async Task<Domain.Product?> Get(Guid id)
        {
            return await _productRepository.Get(id);
        }

        public async Task<List<Domain.Product>?> Get()
        {
            return await _productRepository.Get();
        }

        public async Task Insert(Domain.Product entity)
        {
            if (entity == null)
                throw new ApplicationException("Product entity should not be null");
            
            await _productRepository.Insert(entity);
        }

        public async Task InsertList(IEnumerable<Domain.Product> list)
        {
            if (list == null)
                throw new ApplicationException("Product entity list should not be null");
            
            if (list.Count() == 0)
                throw new ApplicationException("Product entity list should have at least an item");

            await _productRepository.InsertList(list);
        }

        public async Task<List<Domain.Product>?> ListByFilter(ProductFilter filter)
        {
            return await _productRepository.ListByFilter(filter);
        }

        public async Task Update(Domain.Product entity)
        {
            if (entity == null)
                throw new ApplicationException("Product entity should not be null");

            await _productRepository.Update(entity);
        }
    }
}