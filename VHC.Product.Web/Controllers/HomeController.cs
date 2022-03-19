using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VHC.Product.Infrastructure.Config;
using VHC.Product.Web.Models;

namespace VHC.Product.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductApiSettings _productApiSettings;

        public HomeController(ILogger<HomeController> logger, ProductApiSettings productApiSettings)
        {
            _logger = logger;
            _productApiSettings = productApiSettings;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ProductViewModel model)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_productApiSettings.ProductApiUrl);

                byte[] data;
                using BinaryReader br = new BinaryReader(model.File.OpenReadStream());
                data = br.ReadBytes((int)model.File.OpenReadStream().Length);

                ByteArrayContent bytes = new ByteArrayContent(data);

                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                multiContent.Add(bytes, "file", model.File.FileName);

                HttpResponseMessage response = await client.PostAsync("/api/import/product", multiContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("Fail to upload database file");
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}