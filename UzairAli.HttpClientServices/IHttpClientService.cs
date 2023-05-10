using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace UzairAli.HttpClient;

public interface IHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(string uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(Uri uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);


    Task<object?> GetAsync(string uri, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> GetAsync(string uri, Dictionary<string, string> queryParameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> GetAsync(string uri, object queryParameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);

    Task<TReturnModel?> GetAsync<TReturnModel>(string uri, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> GetAsync<TReturnModel>(string uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> GetAsync<TReturnModel>(string uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> GetAsync<TReturnModel>(Uri uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);



    Task<HttpResponseMessage> PostAsync(string uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(string uri, Dictionary<string, object> parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(string uri, object parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(Uri uri, Dictionary<string, object> parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(Uri uri, object parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);


    Task<object?> PostAsync(string uri, HttpContent httpContent, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> PostAsync(string uri, Dictionary<string, string> parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> PostAsync(string uri, object parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> PostAsync(Uri uri, HttpContent httpContent, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> PostAsync(Uri uri, Dictionary<string, string> parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<object?> PostAsync(Uri uri, object parameters, Type returnType, Dictionary<string, string>? headers = null, CancellationToken ct = default);

    Task<TReturnModel?> PostAsync<TReturnModel>(string uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> PostAsync<TReturnModel>(string uri, Dictionary<string, string> parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> PostAsync<TReturnModel>(string uri, object parameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, HttpContent httpContent, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<TReturnModel?> PostAsync<TReturnModel>(Uri uri, object queryParameters, Dictionary<string, string>? headers = null, CancellationToken ct = default);

    Task<HttpResponseMessage> PutAsync(string uri, object model, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> PutAsync(Uri uri, object model, Dictionary<string, string>? headers = null, CancellationToken ct = default);

    Task<HttpResponseMessage> DeleteAsync(Uri uri, Dictionary<string, string>? headers = null, CancellationToken ct = default);
    Task<HttpResponseMessage> DeleteAsync(string uri, Dictionary<string, string>? headers = null, CancellationToken ct = default);


    Task<string?> SerializeJsonAsync(object? valueObject, Type returnType, JsonSerializerOptions? options = null);
    Task<string?> SerializeJsonAsync<TModel>(TModel? valueObject, JsonSerializerOptions? options = null);

    Task<object?> DeserializeResponseAsync(HttpResponseMessage? result, Type returnType);
    Task<TModel?> DeserializeResponseAsync<TModel>(HttpResponseMessage? result);
    Task<object?> DeserializeJsonAsync(string json, Type returnType, JsonSerializerOptions? options = null);
    Task<TReturn?> DeserializeJsonAsync<TReturn>(string json, JsonSerializerOptions? options = null);


    MultipartFormDataContent GetMultipartFormDataContent(string filePath, IDictionary<string, string>? additionalData = default);
}
