using System;
using System.Text.Json.Serialization;

namespace UzairAli.HttpClient.Test.Models.Responses;
internal class AuthenticationResponse
{
    public bool IsAuthenticated { get; set; }

    public string? Message { get; set; }
    public int? StatusCode { get; set; }
    public string? LoginLogUuid { get; set; }

    [JsonPropertyName("userUuid")]
    public Guid? Uuid { get; set; }

    public string? LevelCode { get; set; }

    [JsonPropertyName("userName")]
    public string? Username { get; set; }

    [JsonPropertyName("userEmail")]
    public string? EmailAddress { get; set; }

    [JsonPropertyName("appPassword")]
    public string? Password { get; set; }


    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsBanned { get; set; }
    public string? BanMessage { get; set; }
    public bool RequireDeviceRegistration { get; set; }
    public string? DeviceRegistrationMessage { get; set; }


    public bool DenyAuthentication { get; set; }
    public string? AuthenticationMessage { get; set; }
    public bool ShowMessageFirstTimeOnly { get; set; }
}
