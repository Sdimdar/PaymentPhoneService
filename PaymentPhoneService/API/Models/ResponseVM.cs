using System.Text.Json.Serialization;

namespace PaymentPhoneService.API.Models;

public class ResponseVM
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}