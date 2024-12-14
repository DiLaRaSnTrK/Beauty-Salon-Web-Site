using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

[Route("AI")]
public class AIController : Controller
{
    // Azure Face API anahtarınız ve endpoint adresinizi burada sabit olarak tanımlayabilirsiniz
    private readonly string _faceApiKey = "4Xt7e2iPDSJO7wh4gROY8p18IFEZIMFyYH92PXK0jaOMMp7GZxxhJQQJ99ALACYeBjFXJ3w3AAAKACOGEkta"; // Azure Face API Key
    private readonly string _faceApiEndpoint = "https://hair.cognitiveservices.azure.com/"; // Azure Face API Endpoint

    // GetRequest - AI Haircut & Color Recommendation Form Sayfasını Yüklemek için kullanılır
    [HttpGet("GetRecommendations")]
    public IActionResult AIRecommendations()
    {
        // Form sayfasını yüklemek için kullanılır
        return View();
    }

    // AnalyzeImage - Yüklenen resmi işleyip öneri döndüren işlev
    [HttpPost("AnalyzeImage")]
    public async Task<IActionResult> AnalyzeImage()
    {
        // Form üzerinden gelen dosyayı alıyoruz
        var file = Request.Form.Files["image"];
        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "No image file provided." });
        }

        try
        {
            // HTTPClient kullanarak Azure API'ye istek gönderiyoruz
            using (var httpClient = new HttpClient())
            {
                // Azure API'ye erişim için gerekli API anahtarını header'a ekliyoruz
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _faceApiKey);

                // Azure API'ye gönderilecek URL
                var url = $"{_faceApiEndpoint}/face/v1.0/detect?returnFaceAttributes=age,gender,hair";
                using (var content = new MultipartFormDataContent())
                {
                    // Yüklenen resmin stream'ini alıyoruz
                    var stream = file.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    content.Add(fileContent, "image", file.FileName);

                    // Azure API'ye istek gönderiyoruz
                    var response = await httpClient.PostAsync(url, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    // Başarısız durum kontrolü
                    if (!response.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Face API error: " + responseString });
                    }

                    // API'den dönen veriyi işliyoruz
                    var faceData = JsonConvert.DeserializeObject<dynamic>(responseString);
                    if (faceData.Count > 0)
                    {
                        // Saç rengi ve tipine göre basit öneri
                        var hairAttributes = faceData[0].faceAttributes.hair;
                        string recommendation;

                        // Öneriler saç tipine göre
                        if (hairAttributes.bald > 0.5)
                        {
                            recommendation = "Consider a clean-shaven look or short hair.";
                        }
                        else if (hairAttributes.hairColor.Count > 0)
                        {
                            recommendation = $"Try a hairstyle emphasizing {hairAttributes.hairColor[0].color}.";
                        }
                        else
                        {
                            recommendation = "You have versatile hair! Explore different styles.";
                        }

                        // Öneriyi JSON formatında geri döndürüyoruz
                        return Json(new { success = true, recommendation });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No face detected in the image." });
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            return Json(new { success = false, message = "Error: " + ex.Message });
        }
    }
}
