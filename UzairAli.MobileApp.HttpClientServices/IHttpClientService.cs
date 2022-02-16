using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UzairAli.HttpClientServices;
public interface IHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string uri, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, string> queryParameters, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(string uri, object queryParameters, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(Uri uri, Dictionary<string, string> queryParameters, CancellationToken ct = default);
    Task<HttpResponseMessage> GetAsync(Uri uri, object queryParameters, CancellationToken ct = default);


    Task<HttpResponseMessage> PostAsync(string uri, HttpContent? httpContent, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(string uri, Dictionary<string, object> parameters, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(string uri, object parameters, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent? httpContent, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(Uri uri, Dictionary<string, object> parameters, CancellationToken ct = default);
    Task<HttpResponseMessage> PostAsync(Uri uri, object parameters, CancellationToken ct = default);



    Task<object?> GetFromJsonAsync(string uri, Type returnType, CancellationToken ct = default);
    Task<object?> GetFromJsonAsync(string uri, Dictionary<string, string> queryParameters, Type returnType, CancellationToken ct = default);
    Task<object?> GetFromJsonAsync(string uri, object queryParameters, Type returnType, CancellationToken ct = default);

    Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(string uri, CancellationToken ct = default);
    Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(string uri, Dictionary<string, string> queryParameters, CancellationToken ct = default);
    Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(string uri, object queryParameters, CancellationToken ct = default);
    Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(Uri uri, CancellationToken ct = default);
    Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters, CancellationToken ct = default);
    Task<TReturnModel?> GetFromJsonAsync<TReturnModel>(Uri uri, object queryParameters, CancellationToken ct = default);



    Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(string uri, HttpContent? httpContent, CancellationToken ct = default);
    Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(string uri, Dictionary<string, string> parameters, CancellationToken ct = default);
    Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(string uri, object parameters, CancellationToken ct = default);
    Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(Uri uri, HttpContent? httpContent, CancellationToken ct = default);
    Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(Uri uri, Dictionary<string, string> queryParameters, CancellationToken ct = default);
    Task<TReturnModel?> PostFromJsonAsync<TReturnModel>(Uri uri, object queryParameters, CancellationToken ct = default);

    Task<HttpResponseMessage> PutAsync(string uri, object model, CancellationToken ct = default);

    Task<HttpResponseMessage> DeleteAsync(Uri uri, CancellationToken ct = default);
    Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken ct = default);


    Task<object?> DeserializeResponseAsync(HttpResponseMessage? result, Type returnType);
    Task<TModel?> DeserializeResponseAsync<TModel>(HttpResponseMessage? result);
}
