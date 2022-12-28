using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace UzairAli.HttpClient.Models.Configurations;

public class HttpClientOptions
{
    public Uri? BaseAddress { get; set; }
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
    public ICollection<MediaTypeWithQualityHeaderValue> Accept { get; } 
        = new List<MediaTypeWithQualityHeaderValue> { new MediaTypeWithQualityHeaderValue("application/json") };
    public Dictionary<string, string> Headers { get; set; } = new();

    public AuthenticationHeaderValue? Authorization { get; set; }

    public Encoding RequestEncoding { get; set; } = Encoding.UTF8;
    public string RequestMediaType { get; set; } = "application/json";
}

