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

    [Fact]
    public async void Should_PostAndReturnNull()
    {
        // Arrange
        string url = "https://www.ikonbusiness.com/api/v5/api/auth/sync";
        AuthenticationResponse? expected = null;

        object request = new
        {
            EmailAddress = "100@ikon.com",
            UpdatedAt = "2022-05-20T21:38:30.301",
            DeviceId = "c00f5d0a1e5980ad",
            BuildVersion = "270"
        };

        // Act
        var actual  = await _httpClient.PostAsync<AuthenticationResponse>(url, request);

        // Assert
        Assert.Equal(expected, actual);
    }

}
