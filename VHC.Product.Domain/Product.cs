using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VHC.Product.Helpers.Domain;

namespace VHC.Product.Domain
{
    public class Product : IDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public Guid ProductGroupId { get; set; }
    }
}