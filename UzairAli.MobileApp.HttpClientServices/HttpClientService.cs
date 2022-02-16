using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using UzairAli.MobileApp.HttpClientServices.Attributes;
using UzairAli.MobileApp.HttpClientServices.Exceptions;
using UzairAli.MobileApp.HttpClientServices.Models.Configurations;

namespace UzairAli.MobileApp.HttpClientServices;
public class HttpClientService : IHttpClientService
{
    #region Properties
    #endregion

    #region Fields
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions? _jsonSerializerOptions;
    private readonly HttpClientOptions _options;
    #endregion

    #region Constructors
    public HttpClientService(Action<HttpClientOptions>? opts)
    {
        _options = new()
        {
            Timeout = TimeSpan.FromSeconds(100),
        };
        _options.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        opts?.Invoke(_options);

        _httpClient = new HttpClient()
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
    public async Task<HttpResponseMessage> GetAsync(string uri,
        Dictionary<string, string> queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, queryParameters, ct: ct);
    }
    public async Task<HttpResponseMessage> GetAsync(string uri,
        object queryParameters, CancellationToken ct = default)

    {
        return await GetInternalAsync(uri, queryParameters, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri,
        Dictionary<string, string> queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), queryParameters, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri,
        object queryParameters, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), queryParameters, ct: ct);
    }
    #endregion

    #region GetFromJsonAsync
    public async Task<object?> GetFromJsonAsync(string uri, Type returnType, CancellationToken ct = default)
    {
        return await GetFromJsonInternalAsync(uri, returnType, ct: ct);
    }
    public async Task<object?> GetFromJsonAsync(string uri,
       Dictionary<string, string> queryParameters, Type returnType, CancellationToken ct = default)
    {
        return await GetFromJsonInternalAsync(uri, returnType, queryParameters, ct);
    }
    public async Task<object?> GetFromJsonAsync(string uri,
       object queryParameters, Type returnType, CancellationToken ct = default)
    {
        return await GetFromJsonInternalAsync(uri, returnType, queryParameters, ct);
    }



    public async Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(string uri, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetFromJsonInternalAsync(uri,
            typeof(TReturnModel), ct: ct);
    }
    public async Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(string uri,
        Dictionary<string, string> queryParameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetFromJsonInternalAsync(uri,
            typeof(TReturnModel), queryParameters, ct);
    }
    public async Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(string uri,
        object queryParameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetFromJsonInternalAsync(uri,
            typeof(TReturnModel), queryParameters, ct);
    }

    public async Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(Uri uri, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetFromJsonInternalAsync(uri.ToString(), typeof(TReturnModel), ct: ct);
    }

    public async Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(Uri uri,
        Dictionary<string, string> queryParameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetFromJsonInternalAsync(uri.ToString(),
            typeof(TReturnModel), queryParameters, ct: ct);
    }

    public async Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(Uri uri,
        object queryParameters, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetFromJsonInternalAsync(uri.ToString(),
            typeof(TReturnModel), queryParameters, ct: ct);
    }
    #endregion

    #region PostAsync
    public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent? httpContent, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, httpContent, ct: ct);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri, Dictionary<string, object> parameters,
        CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        return await PostInternalAsync(uri, httpContent, ct: ct);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri, object parameters, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        return await PostInternalAsync(uri, httpContent, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent? httpContent, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), httpContent, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, Dictionary<string, object> parameters,
        CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), parameters, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, object parameters, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), parameters, ct: ct);
    }
    #endregion

    #region PostFromJsonAsync
    public async Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(string uri, HttpContent? httpContent, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostFromJsonInternalAsync(uri, typeof(TReturnModel), httpContent, ct: ct);
    }
    public async Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(string uri,
        Dictionary<string, string> parameters, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        return (TReturnModel?)await PostFromJsonInternalAsync(uri, typeof(TReturnModel), httpContent, ct);
    }
    public async Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(string uri,
        object parameters, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        return (TReturnModel?)await PostFromJsonInternalAsync(uri, typeof(TReturnModel), httpContent, ct);
    }

    public async Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(Uri uri, HttpContent? httpContent, CancellationToken ct = default)
    {
        return await PostFromJsonAsync<TReturnModel>(uri.ToString(), httpContent, ct: ct);
    }

    public async Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters,
        CancellationToken ct = default)
    {
        return await PostFromJsonAsync<TReturnModel>(uri.ToString(), queryParameters, ct: ct);
    }

    public async Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(Uri uri, object queryParameters,
        CancellationToken ct = default)
    {
        return await PostFromJsonAsync<TReturnModel>(uri.ToString(), queryParameters, ct: ct);
    }
    #endregion

    #region HttpPut
    public async Task<HttpResponseMessage> PutAsync(string uri, object model, CancellationToken ct = default)
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

            string? data = await result
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            if (string.IsNullOrEmpty(data))
            {
                return default;
            }

            return await Task.Run(() => JsonSerializer.Deserialize(data, returnType, options: _jsonSerializerOptions));
        }
        catch (Exception ex)
        {
            throw new JsonDeserializeException($"Unable to deserialize object:{returnType.Name}; {ex.Message}", returnType);
        }
    }
    public async Task<TModel?> DeserializeResponseAsync<TModel>(HttpResponseMessage? result)
    {
        return (TModel?)await DeserializeResponseAsync(result, typeof(TModel));
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

    private async Task<object?> GetFromJsonInternalAsync(string uri, Type returnModel,
        object? queryParameters = null, CancellationToken ct = default)
    {
        string responseText = string.Empty;

        var response = await GetInternalAsync(uri, queryParameters, ct);
        if (response is null || response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return default;
        }
#if DEBUG
        responseText = await response.Content.ReadAsStringAsync();
#endif
        return await response.Content.ReadFromJsonAsync(returnModel, _jsonSerializerOptions, cancellationToken: ct);
    }

    private async Task<HttpResponseMessage> PostInternalAsync(string uri, HttpContent? httpContent, CancellationToken ct = default)
    {
        return await _httpClient.PostAsync(uri, httpContent, ct);
    }
    private async Task<object?> PostFromJsonInternalAsync(string uri, Type returnModel, HttpContent? httpContent,
        CancellationToken ct = default)
    {
        var response = await PostInternalAsync(uri, httpContent, ct);
        if (response is null)
        {
            return new();
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