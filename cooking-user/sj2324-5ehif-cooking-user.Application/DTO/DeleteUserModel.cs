namespace sj2324_5ehif_cooking_user.Application.DTO;

public record DeleteUserModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}