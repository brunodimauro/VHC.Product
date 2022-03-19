using System.ComponentModel.DataAnnotations;

namespace VHC.Product.Web.Models
{
    public class ProductViewModel
    {
        [Required]
        [FileExtensions(Extensions = "json", ErrorMessage = "Specify a JSON file.")]
        public IFormFile File { get; set; }
    }
}
