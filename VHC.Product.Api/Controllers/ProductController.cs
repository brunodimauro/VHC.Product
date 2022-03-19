using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using VHC.Product.Api.Dto;
using VHC.Product.Domain;
using VHC.Product.Services;

namespace VHC.Product.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        private string ConsolidateModelState(ModelStateDictionary modelStateDictionary)
        {
            return string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
        }

        [HttpPost]
        [Route("api/product")]
        public async Task<IActionResult> Create(ProductCreateDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Domain.Product product = new Domain.Product
                    {
                        Name = dto.Name,
                        Price = dto.Price,
                        Currency = dto.Currency,
                        ProductGroupId = dto.ProductGroupId
                    };
                    await _productService.Insert(product);

                    return Ok();
                }
                else
                {
                    return BadRequest(ConsolidateModelState(ModelState));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/product")]
        public async Task<IActionResult> Update(Domain.Product product)
        {
            try
            {
                await _productService.Update(product);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/product")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/product/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Domain.Product? product = await _productService.Get(id);

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/product")]
        public async Task<IActionResult> ListByFilter(Guid? id = null, string? name = null)
        {
            try
            {
                Domain.ProductFilter filter = new Domain.ProductFilter
                {
                    ProductId = id,
                    Name = name
                };
                List<Domain.Product>? products = await _productService.ListByFilter(filter);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}