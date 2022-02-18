using System;

using UzairAli.HttpClient.Test.Models;

using Xunit;

namespace UzairAli.HttpClient.Test;
public class DeserializationTests
{
    private readonly HttpClientService _httpClient;

    public DeserializationTests()
    {
        _httpClient = new HttpClientService();
    }

    [Fact]
    public async void Should_DeserializeGuid()
    {
        // Arrange
        string expected = "d3aa2d1a-bb3e-409f-afaf-0f3fe566a903";

        // Act
        string json = /*lang=json,strict*/ "{\"Guid\": \"d3aa2d1a-bb3e-409f-afaf-0f3fe566a903\"}";
        var obj = await _httpClient.DeserializeJsonAsync<GuidTestDto>(json);
        string? actual = obj?.Guid.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeNullableGuid()
    {
        // Arrange
        string? expected = null;

        // Act
        string json = /*lang=json,strict*/ "{\"Guid\": null}";
        var obj = await _httpClient.DeserializeJsonAsync<NullableGuidTestDto>(json);
        string? actual = obj?.Guid?.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeDateTime()
    {
        // Arrange
        DateTime expected = new(2022, 02, 18, 16, 16, 0, DateTimeKind.Utc);

        // Act
        string json = /*lang=json,strict*/ "{\"DateTime\": \"2022-02-18T16:16:00Z\"}";
        var obj = await _httpClient.DeserializeJsonAsync<DateTimeTestDto>(json);
        DateTime? actual = obj?.DateTime;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeNullableDateTime()
    {
        // Arrange
        DateTime? expected = null;

        // Act
        string json = /*lang=json,strict*/ "{\"DateTime\": null }";
        var obj = await _httpClient.DeserializeJsonAsync<NullableDateTimeTestDto>(json);
        DateTime? actual = obj?.DateTime;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeInt()
    {
        // Arrange
        int expected = 5;

        // Act
        string json = /*lang=json,strict*/ "{\"Int\": \"5\" }";
        var obj = await _httpClient.DeserializeJsonAsync<IntTestDto>(json);
        int? actual = obj?.Int;

        // Assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public async void Should_DeserializeNullableInt()
    {
        // Arrange
        int? expected = null;

        // Act
        string json = /*lang=json,strict*/ "{\"Int\": null }";
        var obj = await _httpClient.DeserializeJsonAsync<NullableIntTestDto>(json);
        int? actual = obj?.Int;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeDouble()
    {
        // Arrange
        double expected = 5;

        // Act
        string json = /*lang=json,strict*/ "{\"Double\": \"5\" }";
        var obj = await _httpClient.DeserializeJsonAsync<DoubleTestDto>(json);
        double? actual = obj?.Double;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeNullableDouble()
    {
        // Arrange
        double? expected = null;

        // Act
        string json = /*lang=json,strict*/ "{\"Double\": null }";
        var obj = await _httpClient.DeserializeJsonAsync<NullableDoubleTestDto>(json);
        double? actual = obj?.Double;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Y")]
    [InlineData("y")]
    [InlineData("YES")]
    [InlineData("T")]
    [InlineData("TRUE")]
    [InlineData("true")]
    [InlineData("1")]
    public async void Should_DeserializeStringBooleanToTrue(string stringValue)
    {
        // Arrange
        bool expected = true;

        // Act
        string json = /*lang=json,strict*/ $"{{\"Bool\": \"{stringValue}\" }}";
        var obj = await _httpClient.DeserializeJsonAsync<BooleanTestDto>(json);
        bool? actual = obj?.Bool;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeNullableBoolean()
    {
        // Arrange
        bool? expected = null;

        // Act
        string json = /*lang=json,strict*/ $"{{\"Bool\": null }}";
        var obj = await _httpClient.DeserializeJsonAsync<NullableBooleanTestDto>(json);
        bool? actual = obj?.Bool;

        // Assert
        Assert.Equal(expected, actual);
    }

#if NET6_0_OR_GREATER
    [Fact]
    public async void Should_DeserializeDateOnly()
    {
        // Arrange
        DateOnly expected = new(2022, 02, 18);

        // Act
        string json = /*lang=json,strict*/ $"{{\"DateOnly\": \"2022-02-18\" }}";
        var obj = await _httpClient.DeserializeJsonAsync<DateOnlyTestDro>(json);
        DateOnly? actual = obj?.DateOnly;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeNullableDateOnly()
    {
        // Arrange
        DateOnly? expected = null;

        // Act
        string json = /*lang=json,strict*/ $"{{\"DateOnly\": null }}";
        var obj = await _httpClient.DeserializeJsonAsync<NullableDateOnlyTestDro>(json);
        DateOnly? actual = obj?.DateOnly;

        // Assert
        Assert.Equal(expected, actual);
    }


    [Fact]
    public async void Should_DeserializeTimeOnly()
    {
        // Arrange
        TimeOnly expected = new(16, 56, 0);

        // Act
        string json = /*lang=json,strict*/ $"{{\"TimeOnly\": \"16:56:00\" }}";
        var obj = await _httpClient.DeserializeJsonAsync<TimeOnlyTestDto>(json);
        TimeOnly? actual = obj?.TimeOnly;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async void Should_DeserializeNullableTimeOnly()
    {
        // Arrange
        TimeOnly? expected = null;

        // Act
        string json = /*lang=json,strict*/ $"{{\"TimeOnly\": null }}";
        var obj = await _httpClient.DeserializeJsonAsync<NullableTimeOnlyTestDto>(json);
        TimeOnly? actual = obj?.TimeOnly;

        // Assert
        Assert.Equal(expected, actual);
    }
#endif
}
