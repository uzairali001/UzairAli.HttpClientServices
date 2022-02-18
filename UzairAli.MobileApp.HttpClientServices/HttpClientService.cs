using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using UzairAli.HttpClient.Attributes;
using UzairAli.HttpClient.Exceptions;
using UzairAli.HttpClient.Models.Configurations;
using UzairAli.JsonConverters;

using NetHttpClient = System.Net.Http.HttpClient;

namespace UzairAli.HttpClient;

public class HttpClientService : IHttpClientService
{
    #region Properties
    #endregion

    #region Fields
    private readonly NetHttpClient _httpClient;
    private readonly JsonSerializerOptions? _jsonSerializerOptions;
    private readonly HttpClientOptions _options;
    #endregion

    #region Constructors
    public HttpClientService(Action<HttpClientOptions>? opts = null)
    {
        _options = new()
        {
            Timeout = TimeSpan.FromSeconds(100),
            RequestMediaType = "application/json",
            RequestEncoding = Encoding.UTF8,
            Accept = new List<MediaTypeWithQualityHeaderValue> {
                new MediaTypeWithQualityHeaderValue("application/json")
            },
            JsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                Converters =
                {
#if NET6_0_OR_GREATER
                    new JsonStringDateOnlyConverter(),
                    new JsonStringTimeOnlyConverter(),
#endif
                    new JsonStringBooleanConverter(),
                }
            }
        };

        opts?.Invoke(_options);

        _httpClient = new NetHttpClient()
        {
            BaseAddress = _options.BaseAddress,
            Timeout = _options.Timeout,
        };

        _options.Accept?.ToList()
            .ForEach(_httpClient.DefaultRequestHeaders.Accept.Add);

        if (_options.Authorization is not null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = _options.Authorization;
        }

        _jsonSerializerOptions = _options.JsonOptions;
    }
    #endregion

    #region Public Methods
    #region GetAsync
    public async Task<HttpResponseMessage> GetAsync(string uri, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, ct: ct);
    }
    public async Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, queryParameters, ct: ct);
    }
    public async Task<HttpResponseMessage> GetAsync(string uri, object queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, queryParameters, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string> queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), queryParameters, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, object queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), queryParameters, ct: ct);
    }
    #endregion

    #region GetFromJsonAsync
    public async Task<object?> GetAsync(string uri, Type returnType, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, returnType, ct: ct);
    }
    public async Task<object?> GetAsync(string uri, Dictionary<string, string> queryParameters, Type returnType,
        CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, returnType, queryParameters, ct);
    }
    public async Task<object?> GetAsync(string uri, object queryParameters, Type returnType, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, returnType, queryParameters, ct);
    }


    public async Task<TReturnModel?> GetAsync<TReturnModel>(string uri, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri, typeof(TReturnModel), ct: ct);
    }
    public async Task<TReturnModel?> GetAsync<TReturnModel>(string uri, Dictionary<string, string> queryParameters,
        CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri, typeof(TReturnModel), queryParameters, ct);
    }
    public async Task<TReturnModel?> GetAsync<TReturnModel>(string uri, object queryParameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri, typeof(TReturnModel), queryParameters, ct);
    }

    public async Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri.ToString(), typeof(TReturnModel), ct: ct);
    }

    public async Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters,
        CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri.ToString(), typeof(TReturnModel), queryParameters, ct: ct);
    }

    public async Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, object queryParameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri.ToString(), typeof(TReturnModel), queryParameters, ct: ct);
    }
    #endregion

    #region PostAsync
    public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent httpContent, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, httpContent, ct: ct);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri, Dictionary<string, object> parameters, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, parameters, ct: ct);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri, object parameters, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, parameters, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent httpContent, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri.ToString(), httpContent, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, Dictionary<string, object> parameters,
        CancellationToken ct = default)
    {
        return await PostInternalAsync(uri.ToString(), parameters, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, object parameters, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri.ToString(), parameters, ct: ct);
    }
    #endregion

    #region PostFromJsonAsync
    public async Task<object?> PostAsync(string uri, HttpContent httpContent, Type returnType, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, returnType, httpContent, ct: ct);
    }

    public async Task<object?> PostAsync(string uri, Dictionary<string, string> parameters, Type returnType, CancellationToken ct = default)
    {
        var json = await SerializeJsonAsync(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, _options.RequestEncoding, _options.RequestMediaType);
        
        return await PostInternalAsync(uri, returnType, httpContent, ct);
    }

    public async Task<object?> PostAsync(string uri, object parameters, Type returnType, CancellationToken ct = default)
    {
        var json = await SerializeJsonAsync(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, _options.RequestEncoding, _options.RequestMediaType);

        return await PostInternalAsync(uri, returnType, httpContent, ct);
    }

    public async Task<object?> PostAsync(Uri uri, HttpContent httpContent, Type returnType, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), httpContent, returnType, ct);
    }

    public async Task<object?> PostAsync(Uri uri, Dictionary<string, string> parameters, Type returnType, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), parameters, returnType, ct);
    }

    public async Task<object?> PostAsync(Uri uri, object parameters, Type returnType, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), parameters, returnType, ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(string uri, HttpContent httpContent, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostInternalAsync(uri, typeof(TReturnModel), httpContent, ct: ct);
    }
    public async Task<TReturnModel?> PostAsync<TReturnModel>(string uri, Dictionary<string, string> parameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostAsync(uri, parameters, typeof(TReturnModel), ct);
    }
    public async Task<TReturnModel?> PostAsync<TReturnModel>(string uri, object parameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostAsync(uri, parameters, typeof(TReturnModel), ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, HttpContent httpContent, CancellationToken ct = default)
    {
        return await PostAsync<TReturnModel>(uri.ToString(), httpContent, ct: ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters,
        CancellationToken ct = default)
    {
        return await PostAsync<TReturnModel>(uri.ToString(), queryParameters, ct: ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, object queryParameters,
        CancellationToken ct = default)
    {
        return await PostAsync<TReturnModel>(uri.ToString(), queryParameters, ct: ct);
    }
    #endregion

    #region HttpPut
    public async Task<HttpResponseMessage> PutAsync(string uri, object model, CancellationToken ct = default)
    {
        return await _httpClient.PutAsJsonAsync(uri, model, _jsonSerializerOptions, ct);
    }
    public async Task<HttpResponseMessage> PutAsync(Uri uri, object model, CancellationToken ct = default)
    {
        return await _httpClient.PutAsJsonAsync(uri, model, _jsonSerializerOptions, ct);
    }
    #endregion

    #region HttpDelete
    public async Task<HttpResponseMessage> DeleteAsync(Uri uri, CancellationToken ct = default)
    {
        return await DeleteAsync(uri.ToString(), ct);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken ct = default)
    {
        return await _httpClient.DeleteAsync(uri, ct);
    }
    #endregion

    #region JsonMapping
    public async Task<object?> DeserializeResponseAsync(HttpResponseMessage? result, Type returnType)
    {
        try
        {
            if (result is null)
            {
                return default;
            }

            string? stringContent = await result
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            if (string.IsNullOrEmpty(stringContent))
            {
                return default;
            }

            return await DeserializeJsonAsync(stringContent, returnType, options: _jsonSerializerOptions);
        }
        catch (Exception ex)
        {
            throw new JsonDeserializeException($"Unable to deserialize object:{returnType.Name}", returnType, ex);
        }
    }
    public async Task<TModel?> DeserializeResponseAsync<TModel>(HttpResponseMessage? result)
    {
        return (TModel?)await DeserializeResponseAsync(result, typeof(TModel));
    }

    public async Task<object?> DeserializeJsonAsync(string json, Type returnType, JsonSerializerOptions? options = null)
    {
        return await Task.Run(() => JsonSerializer.Deserialize(json, returnType, options: options ?? _jsonSerializerOptions));
    }
    public async Task<TReturn?> DeserializeJsonAsync<TReturn>(string json, JsonSerializerOptions? options = null)
    {
        return await Task.Run(() => JsonSerializer.Deserialize<TReturn>(json, options: options ?? _jsonSerializerOptions));
    }

    public async Task<string?> SerializeJsonAsync(object? valueObject, Type returnType, JsonSerializerOptions? options = null)
    {
        return await Task.Run(() => JsonSerializer.Serialize(valueObject, returnType, options: options ?? _jsonSerializerOptions));
    }
    public async Task<string?> SerializeJsonAsync<TModel>(TModel? valueObject, JsonSerializerOptions? options = null)
    {
        return await Task.Run(() => JsonSerializer.Serialize(valueObject, options: options ?? _jsonSerializerOptions));
    }
    #endregion

    #endregion

    #region Private Methods
    private async Task<HttpResponseMessage> GetInternalAsync(string uri, object? queryParameters = null,
        CancellationToken ct = default)
    {
        string parameterString = await GetParameterStringAsync(queryParameters);
        string parameterAppend = string.IsNullOrEmpty(parameterString) is false ? "?" + parameterString : string.Empty;
        string uriWithParameters = $"{uri}{parameterAppend}";

        return await _httpClient.GetAsync(uriWithParameters, cancellationToken: ct);
    }

    private async Task<object?> GetInternalAsync(string uri, Type returnModel, object? queryParameters = null,
        CancellationToken ct = default)
    {
        string parameterString = await GetParameterStringAsync(queryParameters);
        string parameterAppend = string.IsNullOrEmpty(parameterString) is false ? "?" + parameterString : string.Empty;
        string uriWithParameters = $"{uri}{parameterAppend}";

        return await _httpClient.GetFromJsonAsync(uriWithParameters, returnModel, _jsonSerializerOptions, ct);
    }

    private async Task<HttpResponseMessage> PostInternalAsync<TRequest>(string uri, TRequest? requestModel, CancellationToken ct = default)
    {
        return await _httpClient.PostAsJsonAsync(uri, requestModel, _jsonSerializerOptions, ct);
    }
    private async Task<object?> PostInternalAsync(string uri, Type returnModel, HttpContent httpContent, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsync(uri, httpContent, ct);
        if (response is null)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync(returnModel, _jsonSerializerOptions, cancellationToken: ct);
    }


    private async Task<string> GetParameterStringAsync(object? queryParameters)
        => await (queryParameters switch
        {
            string para => Task.FromResult(para),
            Dictionary<string, string> para => GetFormUrlEncodedContentAsync(para),
            object para => GetParameterStringFromModelAsync(para),
            _ => Task.FromResult(string.Empty),
        });

    private async Task<string> GetFormUrlEncodedContentAsync(Dictionary<string, string> queryParameters)
    {
        using HttpContent content = new FormUrlEncodedContent(queryParameters!);
        return await content.ReadAsStringAsync();
    }
    private async Task<string> GetParameterStringFromModelAsync(object model)
    {
        Dictionary<string, string> paramDictionary = model.GetType().GetProperties()
            .Where(p => p.GetSetMethod() != null)
            .Where(p => p.GetCustomAttribute<QueryIgnoreAttribute>() is null)
            .Select(p =>
            {
                var propertyName = p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? p.Name;
                object? valueObject = p.GetValue(model, null);
                return new { Name = propertyName, Value = GetParameterValueString(valueObject) };
            })
            .ToDictionary(t => t.Name, t => t.Value);

        return await GetParameterStringAsync(paramDictionary);
    }
    private string GetParameterValueString(object valueObject)
    {
        if (valueObject is null)
        {
            return string.Empty;
        }

        string value = valueObject switch
        {
            //var number when
            //    number is double ||
            //    number is int ||
            //    number is float => Convert.ChangeType(number, number.GetType()),
            var b when
                b is bool bVal => bVal.ToString(),
            var dt when
                dt is DateTime time => time.ToString("o"),
            _ => valueObject.ToString(),
        };
        return value;
    }


    #endregion
}
