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

    [JsonPropertyName("age")]
    public int Age { get; set; }
}
