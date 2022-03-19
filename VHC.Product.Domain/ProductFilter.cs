using VHC.Product.Helpers.Domain;

namespace VHC.Product.Domain
{
    public class ProductFilter : IFilter
    {
        public Guid? ProductId { get; set; } = null;
        public string? Name { get; set; } = null;
    }
}
