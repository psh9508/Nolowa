using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies.Base
{
    public class ServiceBase
    {
        protected static readonly HttpClient httpClient = new HttpClient();

        public ServiceBase()
        {
            httpClient.BaseAddress = new Uri("http://localhost:8080/");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<TModel> GetTFromService<TModel>(string uri)
        {
            if (uri.StartsWith("/"))
                uri = uri.Remove(0, 1);

            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TModel>();

            throw new Exception();
        }

        protected async Task<TResult> DoPost<TResult, TRequest>(string uri, TRequest body)
        {
            if (uri.StartsWith("/"))
                uri = uri.Remove(0, 1);

            var response = await httpClient.PostAsJsonAsync(uri, body);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<TResult>();

            throw new Exception();
        }
    }
}
