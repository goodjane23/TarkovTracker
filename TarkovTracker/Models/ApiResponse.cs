using System.Text.Json.Serialization;

namespace TarkovTracker.Models;

public class ApiResponse
{
    [JsonPropertyName("data")]
    public Hideout Data { get; set; }
}
