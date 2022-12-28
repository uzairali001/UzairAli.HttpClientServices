using System;
using System.Net.Http;
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;

using UzairAli.HttpClient.Models.Configurations;
using UzairAli.JsonConverters;

namespace UzairAli.HttpClient.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClientService(this IServiceCollection services, Action<HttpClientOptions>? httpOptionsBuilder = default,
        Action<JsonSerializerOptions>? jsonOptionsBuilder = default)
    {
        //Action<HttpClientOptions>? httpOptions = null, Action< JsonSerializerOptions >? jsonOptions = null

        services.AddSingleton<IHttpClientService>(provider =>
        {
            JsonSerializerOptions jsonOptions = new()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                Converters =
            {
#if NET6_0_OR_GREATER
                new JsonStringDateOnlyConverter(),
                new JsonStringTimeOnlyConverter(),
#endif
                new JsonStringBooleanConverter(),
                new JsonStringGuidConverter(),
                new JsonStringDateTimeConverter(),
                new JsonStringDoubleConverter(),
                new JsonStringIntConverter(),
            }
            };
            jsonOptionsBuilder?.Invoke(jsonOptions);

            HttpClientOptions httpOptions = new();
            httpOptionsBuilder?.Invoke(httpOptions);

            return new HttpClientService(httpOptions, jsonOptions);
        });

        return services;
    }
}
