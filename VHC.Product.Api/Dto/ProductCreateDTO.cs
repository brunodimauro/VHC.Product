using System.ComponentModel.DataAnnotations;

namespace VHC.Product.Api.Dto
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public Guid ProductGroupId { get; set; }

        [Required]
        public string Currency { get; set; }
    }
}
