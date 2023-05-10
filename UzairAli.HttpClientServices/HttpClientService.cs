using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using UzairAli.HttpClient.Attributes;
using UzairAli.HttpClient.Exceptions;
using UzairAli.HttpClient.Models;
using UzairAli.HttpClient.Models.Configurations;

//using NetHttpClient = System.Net.Http.HttpClient;

namespace UzairAli.HttpClient;

public class HttpClientService : IHttpClientService
{
    #region Properties
    #endregion

    #region Fields
    //private readonly NetHttpClient _httpClient;
    private readonly JsonSerializerOptions? _jsonSerializerOptions;
    private readonly HttpClientOptions _options;

    private readonly SocketsHttpHandler _socketsHandler;
    #endregion

    #region Constructors
    public HttpClientService(HttpClientOptions? httpOptions = default, JsonSerializerOptions? jsonOptions = default)
    {
        _options = httpOptions ?? new();
        _jsonSerializerOptions = jsonOptions ?? new();

        _socketsHandler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(10),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
            MaxConnectionsPerServer = 10
        };
    }
    #endregion

    #region Public Methods
    #region GetAsync
    public async Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, headers, ct: ct);
    }
    public async Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, queryParameters, headers, ct: ct);
    }
    public async Task<HttpResponseMessage> GetAsync(string uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, queryParameters, headers, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), headers, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), queryParameters, headers, ct: ct);
    }

    public async Task<HttpResponseMessage> GetAsync(Uri uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri.ToString(), queryParameters, headers, ct: ct);
    }
    #endregion

    #region GetFromJsonAsync
    public async Task<object?> GetAsync(string uri, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, returnType, headers, ct: ct);
    }
    public async Task<object?> GetAsync(string uri, Dictionary<string, string> queryParameters, Type returnType, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, returnType, queryParameters, headers, ct);
    }
    public async Task<object?> GetAsync(string uri, object queryParameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetInternalAsync(uri, returnType, queryParameters, headers, ct);
    }


    public async Task<TReturnModel?> GetAsync<TReturnModel>(string uri, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri, typeof(TReturnModel), headers: headers, ct: ct);
    }
    public async Task<TReturnModel?> GetAsync<TReturnModel>(string uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri, typeof(TReturnModel), queryParameters, headers, ct);
    }
    public async Task<TReturnModel?> GetAsync<TReturnModel>(string uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri, typeof(TReturnModel), queryParameters, headers, ct);
    }

    public async Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri.ToString(), typeof(TReturnModel), ct: ct);
    }

    public async Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri.ToString(), typeof(TReturnModel), queryParameters, headers, ct: ct);
    }

    public async Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await GetInternalAsync(uri.ToString(), typeof(TReturnModel), queryParameters, headers, ct: ct);
    }
    #endregion

    #region PostAsync
    public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, httpContent, headers, ct: ct);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri, Dictionary<string, object> parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, parameters, headers, ct: ct);
    }
    public async Task<HttpResponseMessage> PostAsync(string uri, object parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, parameters, headers, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri.ToString(), httpContent, headers, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, Dictionary<string, object> parameters, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        return await PostInternalAsync(uri.ToString(), parameters, headers, ct: ct);
    }

    public async Task<HttpResponseMessage> PostAsync(Uri uri, object parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri.ToString(), parameters, headers, ct: ct);
    }
    #endregion

    #region PostFromJsonAsync
    public async Task<object?> PostAsync(string uri, HttpContent httpContent, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostInternalAsync(uri, returnType, httpContent, headers, ct: ct);
    }

    public async Task<object?> PostAsync(string uri, Dictionary<string, string> parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        var json = await SerializeJsonAsync(parameters, options: _jsonSerializerOptions);
        var httpContent = new StringContent(json, _options.RequestEncoding, _options.RequestMediaType);

        return await PostInternalAsync(uri, returnType, httpContent, headers, ct);
    }

    public async Task<object?> PostAsync(string uri, object parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        string? json = await SerializeJsonAsync(parameters, options: _jsonSerializerOptions) ?? string.Empty;
        var httpContent = new StringContent(json, _options.RequestEncoding, _options.RequestMediaType);

        return await PostInternalAsync(uri, returnType, httpContent, headers, ct);
    }

    public async Task<object?> PostAsync(Uri uri, HttpContent httpContent, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), httpContent, returnType, headers, ct);
    }

    public async Task<object?> PostAsync(Uri uri, Dictionary<string, string> parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), parameters, returnType, headers, ct);
    }

    public async Task<object?> PostAsync(Uri uri, object parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostAsync(uri.ToString(), parameters, returnType, headers, ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(string uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostInternalAsync(uri, typeof(TReturnModel), httpContent, ct: ct);
    }
    public async Task<TReturnModel?> PostAsync<TReturnModel>(string uri, Dictionary<string, string> parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostAsync(uri, parameters, typeof(TReturnModel), headers, ct);
    }
    public async Task<TReturnModel?> PostAsync<TReturnModel>(string uri, object parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return (TReturnModel?)await PostAsync(uri, parameters, typeof(TReturnModel), headers, ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await PostAsync<TReturnModel>(uri.ToString(), httpContent, headers, ct: ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        return await PostAsync<TReturnModel>(uri.ToString(), queryParameters, headers, ct: ct);
    }

    public async Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, object queryParameters, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        return await PostAsync<TReturnModel>(uri.ToString(), queryParameters, headers, ct: ct);
    }
    #endregion

    #region HttpPut
    public async Task<HttpResponseMessage> PutAsync(string uri, object model, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetHttpClient(headers).PutAsJsonAsync(uri, model, _jsonSerializerOptions, ct);
    }
    public async Task<HttpResponseMessage> PutAsync(Uri uri, object model, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetHttpClient(headers).PutAsJsonAsync(uri, model, _jsonSerializerOptions, ct);
    }
    #endregion

    #region HttpDelete
    public async Task<HttpResponseMessage> DeleteAsync(Uri uri, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await DeleteAsync(uri.ToString(), headers, ct);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string uri, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetHttpClient(headers).DeleteAsync(uri, ct);
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

    #region HTTPContent
    public MultipartFormDataContent GetMultipartFormDataContent(string filePath, IDictionary<string, string>? additionalData = default)
    {
        //Add the file
        var stream = File.OpenRead(filePath);

        MultipartFormDataContent multipartFormContent = new();
        multipartFormContent.Headers.ContentType.MediaType = "multipart/form-data";

        //Add additional fields if any
        AppendAdditionalData(additionalData, multipartFormContent);
        multipartFormContent.Add(new StreamContent(stream), name: "file", fileName: Path.GetFileName(filePath));
        return multipartFormContent;
    }

    private static void AppendAdditionalData(IDictionary<string, string>? additionalData, MultipartFormDataContent multipartFormContent)
    {
        if (additionalData is not null)
        {
            foreach (var data in additionalData)
            {
                multipartFormContent.Add(new StringContent(data.Value), name: data.Key);
            }
        }
    }

    #region File Upload 
    public async Task UploadFileAsync(string apiEndpoint, FileMeta file, IDictionary<string, string>? additionalData = default,
        AuthenticationHeaderValue? authorization = default, int bufferSize = 100000)
    {
        await UploadFileAsync(apiEndpoint, new List<FileMeta>()
        {
            new FileMeta()
            {
                Name = file.Name,
                Path = file.Path
            }
        }, additionalData, authorization, bufferSize);
    }
    public async Task UploadFileAsync(string apiEndpoint, string file, IDictionary<string, string>? additionalData = default,
        AuthenticationHeaderValue? authorization = default, int bufferSize = 100000)
    {
        await UploadFileAsync(apiEndpoint, new List<string>() { file }, additionalData, authorization, bufferSize);
    }
    public async Task UploadFileAsync(string apiEndpoint, IEnumerable<string> files, IDictionary<string, string>? additionalData = default,
        AuthenticationHeaderValue? authorization = default, int bufferSize = 100000)
    {
        await UploadFileAsync(apiEndpoint, files.Select(f => new FileMeta()
        {
            Name = Path.GetFileName(f),
            Path = f,
        }), additionalData, authorization, bufferSize);
    }
    public async Task UploadFileAsync(string apiEndpoint, IEnumerable<FileMeta> files, IDictionary<string, string>? additionalData = default,
        AuthenticationHeaderValue? authorization = default, int bufferSize = 100000)
    {
        var httpClient = GetHttpClient();
        var multipartContent = new MultipartFormDataContent("NKdKd9Yk");
        multipartContent.Headers.ContentType.MediaType = "multipart/form-data";
        AppendAdditionalData(additionalData, multipartContent);


        foreach (var file in files)
        {
            var content = new StreamContent(new FileStream(file.Path, FileMode.Open), bufferSize);
            multipartContent.Add(content, file.Name, file.Path);
        }

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        httpClient.DefaultRequestHeaders.Authorization = authorization;
        var response = await httpClient.PostAsync(apiEndpoint, multipartContent);
        response.EnsureSuccessStatusCode();
    }
    #endregion

    #endregion

    #endregion

    #region Private Methods
    private async Task<HttpResponseMessage> GetInternalAsync(string uri, object? queryParameters = null, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        string parameterString = await GetParameterStringAsync(queryParameters);
        string parameterAppend = string.IsNullOrEmpty(parameterString) is false ? "?" + parameterString : string.Empty;
        string uriWithParameters = $"{uri}{parameterAppend}";

        return await GetHttpClient(headers).GetAsync(uriWithParameters, cancellationToken: ct);
    }

    private async Task<object?> GetInternalAsync(string uri, Type returnModel, object? queryParameters = null, Dictionary<string, string>? headers = null,
        CancellationToken ct = default)
    {
        string parameterString = await GetParameterStringAsync(queryParameters);
        string parameterAppend = string.IsNullOrEmpty(parameterString) is false ? "?" + parameterString : string.Empty;
        string uriWithParameters = $"{uri}{parameterAppend}";

        return await GetHttpClient(headers).GetFromJsonAsync(uriWithParameters, returnModel, _jsonSerializerOptions, ct);
    }

    private async Task<HttpResponseMessage> PostInternalAsync<TRequest>(string uri, TRequest? requestModel, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        return await GetHttpClient(headers).PostAsJsonAsync(uri, requestModel, _jsonSerializerOptions, ct);
    }
    private async Task<object?> PostInternalAsync(string uri, Type returnModel, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default)
    {
        HttpResponseMessage response = await GetHttpClient(headers).PostAsync(uri, httpContent, ct);
        string? responseString = await response.Content.ReadAsStringAsync(ct);

        if (string.IsNullOrWhiteSpace(responseString))
        {
            return default;
        }

        try
        {
            return await response.Content.ReadFromJsonAsync(returnModel, _jsonSerializerOptions, cancellationToken: ct);
        }
        catch (JsonException ex)
        {
            var requestString = await httpContent.ReadAsStringAsync();
            throw new InvalidJsonException(uri, requestString, responseString, response.StatusCode, ex);
        }
    }


    private async Task<string> GetParameterStringAsync(object? queryParameters)
        => await (queryParameters switch
        {
            string para => Task.FromResult(para),
            Dictionary<string, string> para => GetFormUrlEncodedContentAsync(para),
            ExpandoObject obj => GetParameterStringFromExpendoObject(obj),
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
            .Where(p => p.GetGetMethod() != null)
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
    private string GetParameterValueString(object? valueObject)
    {
        if (valueObject is null)
        {
            return string.Empty;
        }

        var value = valueObject switch
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
        return value ?? string.Empty;
    }
    private async Task<string> GetParameterStringFromExpendoObject(ExpandoObject obj)
    {
        return string.Join("&", obj.ToList().Select(o => $"{o.Key}={GetParameterValueString(o.Value)}"));
    }


    private System.Net.Http.HttpClient GetHttpClient(Dictionary<string, string>? headers = null)
    {
        System.Net.Http.HttpClient client = new(_socketsHandler, disposeHandler: false)
        {
            BaseAddress = _options.BaseAddress,
            Timeout = _options.Timeout
        };

        _options.Accept.ToList().ForEach(client.DefaultRequestHeaders.Accept.Add);
        _options.Headers.ToList().ForEach(header =>
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        });

        if (headers is not null)
        {
            foreach (KeyValuePair<string, string> item in headers)
            {
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }

        if (_options.Authorization is not null)
        {
            client.DefaultRequestHeaders.Authorization = _options.Authorization;
        }

        return client;
    }

    #endregion
}
