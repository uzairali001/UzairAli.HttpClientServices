using System.Text.Json.Serialization;

namespace UzairAli.HttpClient.Test.Models.Requests;
internal class ApplicationDetailRequest
{
    // Application Build Number (1)
    [JsonPropertyName("build")]
    public string Build { get; set; } = null!;

    // Application Version (1.0.0)
    [JsonPropertyName("version")]
    public string Version { get; set; } = null!;

    // Package Name/Application Identifier (com.microsoft.testapp)
    [JsonPropertyName("packageName")]
    public string PackageName { get; set; } = null!;

    // Application UUID
    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; } = null!;

    // Device Model (SMG-950U, iPhone10,6)
    [JsonPropertyName("deviceModel")]
    public string DeviceModel { get; set; } = null!;

    // Manufacturer (Samsung)
    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; set; } = null!;

    // Device Name (Motz's iPhone)
    [JsonPropertyName("deviceName")]
    public string DeviceName { get; set; } = null!;

    // Platform (Android)
    [JsonPropertyName("platform")]
    public string Platform { get; set; } = null!;

    // Operating System Version android 4.4.4, iOS 10.4
    [JsonPropertyName("osVersion")]
    public string OSVersion { get; set; } = null!;

    // Idiom (Phone)
    [JsonPropertyName("idiom")]
    public string Idiom { get; set; } = null!;

    // Device Type (Physical)
    [JsonPropertyName("deviceType")]
    public string DeviceType { get; set; } = null!;
}
