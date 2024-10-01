using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class FileUploadController : Controller
{
    private readonly HttpClient _httpClient;

    // Inject HttpClient via constructor
    public FileUploadController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError(string.Empty, "Please select a file.");
            return View();
        }

        using (var content = new MultipartFormDataContent())
        {
            var streamContent = new StreamContent(file.OpenReadStream());
            content.Add(streamContent, "file", file.FileName);

            string azureFunctionUrl = "http://localhost:7209/api/UploadFile";

            var response = await _httpClient.PostAsync(azureFunctionUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("FileUploadSuccess");
            }

            ModelState.AddModelError(string.Empty, "Failed to upload file.");
            return View();
        }
    }

    public IActionResult FileUploadSuccess()
    {
        return RedirectToAction("FileUploadSuccess");
    }
}
