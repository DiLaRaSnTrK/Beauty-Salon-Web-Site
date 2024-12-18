using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class DeepAiService
{
    private readonly string _apiKey = "da1a4644-369b-4dc8-83fe-ed3d1a073654"; // API anahtarınızı buraya ekleyin

    public async Task<string> GetHairstyleRecommendationAsync(IFormFile image)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Api-Key", _apiKey);

            var url = "https://api.deepai.org/api/hairstyle";
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(image.OpenReadStream());
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
            content.Add(fileContent, "image", image.FileName);

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }

            return $"Error: {responseString}";
        }
    }
}
