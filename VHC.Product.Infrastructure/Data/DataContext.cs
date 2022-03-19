using Microsoft.EntityFrameworkCore;

namespace VHC.Product.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        private readonly Action<DataContext, ModelBuilder> _modelCustomizer;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext(DbContextOptions<DataContext> options, Action<DataContext, ModelBuilder> modelCustomizer = null)
            : base(options)
        {
            _modelCustomizer = modelCustomizer;
        }

        public DbSet<Domain.Product> Product { get; set; }
    }
}
