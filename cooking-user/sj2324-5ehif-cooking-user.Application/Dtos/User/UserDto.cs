namespace sj2324_5ehif_cooking_user.Application.DTO;

public class UserDto
{
    public string Key { get; set; }
    public string Username { get; set; }
    public string Lastname { get; set; }
    public string Firstname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<PreferenceDto> Preferences { get; set; } = new();
}