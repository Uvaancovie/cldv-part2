using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class ProductController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    // Constructor to inject HttpClient and IConfiguration
    public ProductController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    // GET request to render the AddProduct form
    [HttpGet]
    public IActionResult AddProduct()
    {
        return View();  // This returns the AddProduct.cshtml view
    }

    // POST request to submit product data to the Azure Function
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductViewModel product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);  // Return form if validation fails
        }

        // Convert product to JSON
        var jsonContent = JsonConvert.SerializeObject(product);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Get the Azure Function URL from the appsettings.json
        string azureFunctionUrl = _configuration["AzureFunctionBaseUrl"] + "/api/StoreProduct";

        // Make the POST request to the Azure Function
        var response = await _httpClient.PostAsync(azureFunctionUrl, content);

        if (response.IsSuccessStatusCode)
        {
            // Redirect to a success page if the request is successful
            return RedirectToAction("ProductAddedSuccess");
        }

        // If something goes wrong, return an error
        ModelState.AddModelError(string.Empty, "Failed to add product.");
        return View(product);
    }

    // Success page for product addition
    public IActionResult ProductAddedSuccess()
    {
        return View();
    }
}
