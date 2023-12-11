using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using sj2324_5ehif_cooking_user.Application.Infrastructure;

namespace sj2324_5ehif_cooking_user.Webapi.Services;

public record UserServiceDto(string key, string username);
public interface IUserService
{
    public Task createEvent(UserServiceDto data);
}
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;


    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
    }

    public async Task createEvent(UserServiceDto data)
    {
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        var endpoints = _configuration.GetSection("CommunicationServices:user_endpoints").Get<string[]>();
        foreach (var location in endpoints)
        {
            var x = await _httpClient.PostAsync(location, content);
            
        }
        
    }
}