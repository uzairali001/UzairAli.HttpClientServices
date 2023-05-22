using System.Collections.Generic;

using UzairAli.NetHttpClient;

using Xunit;

namespace UzairAli.HttpClient.Test;
public class HttpGetTests
{
    private readonly HttpClientService _httpClient;
    public HttpGetTests()
    {
        _httpClient = new HttpClientService();
    }

    [Fact]
    public async void TestGetHeaders()
    {
        // Arrange
        string url = "https://jsonplaceholder.typicode.com/posts";


        // Act
        var response = await _httpClient.GetAsync(url, headers: new Dictionary<string, string>()
        {
            {"Authorization", "test"}
        });

        var a = response.RequestMessage.Headers.ToString();

        // Assert
        //Assert.Equal(actual);
    }


}
