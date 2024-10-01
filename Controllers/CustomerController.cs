using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CustomerController : Controller
{
    private readonly HttpClient _httpClient;

    // Inject HttpClient via constructor
    public CustomerController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer(CustomerViewModel customer)
    {
        // Validate the customer data
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        // Convert the customer object to JSON
        var jsonContent = JsonConvert.SerializeObject(customer);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // URL to your StoreCustomer Azure Function
        string azureFunctionUrl = "http://localhost:7209/api/StoreCustomer";

        // Send POST request to the Azure Function
        var response = await _httpClient.PostAsync(azureFunctionUrl, content);

        if (response.IsSuccessStatusCode)
        {
            // If successful, return to a view or redirect
            return RedirectToAction("CustomerAddedSuccess");
        }

        // Handle errors if needed
        ModelState.AddModelError(string.Empty, "Failed to add customer.");
        return View(customer);
    }

    // Example of success view
    public IActionResult CustomerAddedSuccess()
    {
        return RedirectToAction("CustomerAddedSuccess");
    }
}
