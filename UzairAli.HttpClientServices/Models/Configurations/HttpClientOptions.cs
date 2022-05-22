using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace UzairAli.HttpClient.Models.Configurations;

public class HttpClientOptions
{
    public Uri? BaseAddress { get; set; }
    public TimeSpan Timeout { get; set; }
    public IEnumerable<MediaTypeWithQualityHeaderValue> Accept { get; set; } = null!;

    public AuthenticationHeaderValue? Authorization { get; set; }

    public JsonSerializerOptions JsonOptions { get; set; } = null!;

    public Encoding RequestEncoding { get; set; } = null!;
    public string RequestMediaType { get; set; } = null!;
}

