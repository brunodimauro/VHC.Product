using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VHC.Product.Services;

namespace VHC.Product.Api.Controllers
{
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ImportController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        [Route("api/import/product")]
        public async Task<IActionResult> UploadData(IFormFile file)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();

                using StreamReader sr = new StreamReader(file.OpenReadStream());
                using JsonTextReader jsonTextReader = new JsonTextReader(sr);
                List<Domain.Product> products = serializer.Deserialize<List<Domain.Product>>(jsonTextReader);
                await _productService.InsertList(products);

                return Ok("Json file imported to database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
