using System;
using System.Text.Json.Serialization;

namespace UzairAli.HttpClient.Test.Models.Responses;
internal class DoctorResponse
{
    [JsonPropertyName("guid")]
    public Guid DoctorGuid { get; set; }
    public int VisitCount { get; set; }
    public string UserEmail { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public string DoctorCode { get; set; } = null!;
    public string Class { get; set; } = null!;
    public string? Specialty { get; set; }
    public string? WorkingArea { get; set; }

    public bool IsException { get; set; }

    public DateTime? LastVisitDate { get; set; }
    public DateTime? NextVisitDate { get; set; }

    public double? MorningLatitude { get; set; }
    public double? MorningLongitude { get; set; }
    public double? EveningLatitude { get; set; }
    public double? EveningLongitude { get; set; }

    public DateTime LastUpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
