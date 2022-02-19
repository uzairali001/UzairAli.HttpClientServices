using System;
using System.Dynamic;

using UzairAli.HttpClient.Test.Models;

using Xunit;

namespace UzairAli.HttpClient.Test; 
public class SerializationTests
{
    private readonly HttpClientService _httpClient;

    public SerializationTests()
    {
        _httpClient = new HttpClientService();
    }

    [Theory]
    [InlineData("d3aa2d1a-bb3e-409f-afaf-0f3fe566a903")]
    [InlineData("d3aa2d1abb3e409fafaf0f3fe566a903")]
    [InlineData("{d3aa2d1a-bb3e-409f-afaf-0f3fe566a903}")]
    public async void Should_SerializeGuid(string guid)
    {
        // Arrange
        string expected = /*lang=json,strict*/ $"{{\"guid\":\"d3aa2d1a-bb3e-409f-afaf-0f3fe566a903\"}}";

        // Act
        GuidTestDto dto = new() { Guid = Guid.Parse(guid) };
        string? actual = await new HttpClientService().SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeNullableGuid()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"guid\":null}";

        // Act
        NullableGuidTestDto dto = new() { Guid = null };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeDateTime()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"dateTime\":\"2022-02-18T16:16:00.0000000Z\"}";

        // Act
        DateTimeTestDto dto = new() { DateTime = DateTime.Parse("2022-02-18T16:16:00.0000000Z").ToUniversalTime() };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeNullableDateTime()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"dateTime\":null}";

        // Act
        NullableDateTimeTestDto dto = new() { DateTime = null };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeInt()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"int\":5}";

        // Act
        IntTestDto dto = new() { Int = 5 };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public async void Should_SerializeNullableInt()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"int\":null}";

        // Act
        NullableIntTestDto dto = new() { Int = null };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeDouble()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"double\":5}";

        // Act
        DoubleTestDto dto = new() { Double = 5 };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeNullableDouble()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"double\":null}";

        // Act
        NullableDoubleTestDto dto = new() { Double = null };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeBooleanToTrue()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"bool\":true}";

        // Act
        BooleanTestDto dto = new() { Bool = true };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_SerializeNullableBoolean()
    {
        // Arrange
        string expected = /*lang=json,strict*/ "{\"bool\":null}";

        // Act
        NullableBooleanTestDto dto = new() { Bool = null };
        string? actual = await _httpClient.SerializeJsonAsync(dto);

        // Assert
        Assert.Equal(expected, actual);
    }

#if NET6_0_OR_GREATER
            [Fact]
            public async void Should_SerializeDateOnly()
            {
                // Arrange
                string expected = /*lang=json,strict*/ $"{{\"dateOnly\":\"2022-02-18\"}}";

                // Act
                DateOnlyTestDro dto = new() { DateOnly = new(2022, 02, 18) };
                string? actual = await _httpClient.SerializeJsonAsync(dto);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public async void Should_SerializeNullableDateOnly()
            {
                // Arrange
                string expected = /*lang=json,strict*/ $"{{\"dateOnly\":null}}";

                // Act
                NullableDateOnlyTestDro dto = new() { DateOnly = null };
                string? actual = await _httpClient.SerializeJsonAsync(dto);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public async void Should_SerializeTimeOnly()
            {
                // Arrange
                string expected = /*lang=json,strict*/ $"{{\"timeOnly\":\"16:56:00\"}}";

                // Act
                TimeOnlyTestDto dto = new() { TimeOnly = new(16, 56, 0) };
                string? actual = await _httpClient.SerializeJsonAsync(dto);

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public async void Should_SerializeNullableTimeOnly()
            {
                // Arrange
                string expected = /*lang=json,strict*/ $"{{\"timeOnly\":null}}";

                // Act
                NullableTimeOnlyTestDto dto = new() { TimeOnly = null };
                string? actual = await _httpClient.SerializeJsonAsync(dto);

                // Assert
                Assert.Equal(expected, actual);
            }

#endif
}
