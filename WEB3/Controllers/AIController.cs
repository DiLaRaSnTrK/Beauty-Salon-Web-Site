using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class AIController : Controller
{
    private readonly HttpClient _httpClient;

    public AIController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Analyze(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewBag.Error = "Dosya yüklenmedi.";
            return View("Index");
        }

        using var content = new MultipartFormDataContent();
        using var fileStream = file.OpenReadStream();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");
        content.Add(fileContent, "file", file.FileName);

        // Python API'ye istek gönderme
        var response = await _httpClient.PostAsync("http://localhost:5000/analyze", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            ViewBag.Error = $"Hata: {error}";
            Console.WriteLine($"API Error: {error}");
            return View("Index");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API Response: {responseString}");

        // JSON yanıtını JsonDocument ile parse etme
        using (var jsonDoc = JsonDocument.Parse(responseString))
        {
            var root = jsonDoc.RootElement;

            // Saç rengi önerisini alma
            var suggestion = root.GetProperty("suggestion").GetString();
            var avgColor = root.GetProperty("avg_color").EnumerateArray();

            // Konsola yazdırma
            Console.WriteLine($"Suggested Hair Color: {suggestion}");

            // AvgColor dizisini almak ve her bir öğeyi double olarak almak
            var avgColorList = avgColor.Select(c => c.GetDouble()).ToList();
            Console.WriteLine($"AvgColor: {string.Join(", ", avgColorList)}");

            // Sonuçları ViewBag'e aktar
            ViewBag.Suggestion = suggestion;
            ViewBag.AvgColor = avgColorList.Any()
                ? string.Join(", ", avgColorList)
                : "Renk bilgisi bulunamadı.";
        }

        return View("Result");
    }
}
