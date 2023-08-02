using MANAGER.Backend.Core.Constants;
using System.Text.Json.Serialization;

namespace MANAGER.Backend.WebApi.Model;

public class UserInput
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("permissions")]
    public required List<Roles> Permissions { get; set; }
}
