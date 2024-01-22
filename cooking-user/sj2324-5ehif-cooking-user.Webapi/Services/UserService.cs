using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace sj2324_5ehif_cooking_user.Webapi.Services;

public abstract record Dto();
public record UserServiceDto(string key, string username) : Dto();
public interface IInterCallService
{
    public Task createEvent(Dto data);
    public Task deleteEvent(string data);
    
}
public class InterCallService : IInterCallService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;


    public InterCallService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
    }

    public async Task createEvent(Dto data)
    {
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        var endpoints = _configuration.GetSection("CommunicationServices:user_endpoints").Get<string[]>();
        foreach (var location in endpoints)
        {
            await _httpClient.PostAsync(location, content);
            
        }
        
    }
    public async Task deleteEvent(string data)
    {
        var endpoints = _configuration.GetSection("CommunicationServices:user_endpoints").Get<string[]>();
        foreach (var location in endpoints)
        {
            Console.WriteLine($"{location}/{data}");
            var x = await _httpClient.DeleteAsync($"{location}/{data}");
            Console.WriteLine(await x.Content.ReadAsStringAsync());
            
        }
    }
}