using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;

using Xunit;

namespace UzairAli.HttpClient.Test;
public class HttpGetTests
{
    private readonly HttpClientService _httpClient;
    public HttpGetTests()
    {
        _httpClient = new HttpClientService();
    }

}
