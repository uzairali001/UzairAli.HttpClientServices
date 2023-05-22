
/* Unmerged change from project 'UzairAli.NetHttpClient (net5.0)'
Before:
using System;
After:
using Microsoft.Extensions.DependencyInjection;

using System;
*/

/* Unmerged change from project 'UzairAli.NetHttpClient (net7.0)'
Before:
using System;
After:
using Microsoft.Extensions.DependencyInjection;

using System;
*/
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Text.Json;


/* Unmerged change from project 'UzairAli.NetHttpClient (net5.0)'
Before:
using Microsoft.Extensions.DependencyInjection;

using UzairAli.HttpClient;
After:
using UzairAli.HttpClient;
*/

/* Unmerged change from project 'UzairAli.NetHttpClient (net7.0)'
Before:
using Microsoft.Extensions.DependencyInjection;

using UzairAli.HttpClient;
After:
using UzairAli.HttpClient;
*/
using UzairAli.JsonConverters;
using UzairAli.NetHttpClient.Models.Configurations;

namespace UzairAli.NetHttpClient.Extensions;
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
