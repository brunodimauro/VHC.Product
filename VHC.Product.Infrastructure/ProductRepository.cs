using Microsoft.EntityFrameworkCore;
using VHC.Product.Helpers.Repository;
using VHC.Product.Infrastructure.Data;

namespace VHC.Product.Infrastructure
{
    public class ProductRepository : IRepository<Domain.Product, Domain.ProductFilter>
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Delete(Domain.Product entity)
        {
            _context.Product.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Product?> Get(Guid id)
        {
            return await _context.Product.SingleOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<List<Domain.Product>?> Get()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<List<Domain.Product>?> ListByFilter(Domain.ProductFilter filter)
        {
            return await _context.Product
                .Where(x => string.Equals(x.Name, filter.Name, StringComparison.CurrentCultureIgnoreCase) || filter.Name == null)
                .Where(x => x.ProductId == filter.ProductId || filter.ProductId == null)
                .ToListAsync();
        }

        public async Task Insert(Domain.Product entity)
        {
            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertList(IEnumerable<Domain.Product> list)
        {
            await _context.Product.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Domain.Product entity)
        {
            _context.Product.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}