using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;

using UzairAli.HttpClient.Test.Models;
using UzairAli.HttpClient.Test.Models.Requests;
using UzairAli.HttpClient.Test.Models.Responses;

using Xunit;

namespace UzairAli.HttpClient.Test;
public class HttpPostTests
{
    private readonly HttpClientService _httpClient;
    public HttpPostTests()
    {
        _httpClient = new HttpClientService();
    }

}
