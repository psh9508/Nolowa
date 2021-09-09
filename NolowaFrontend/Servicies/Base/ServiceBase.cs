using NolowaFrontend.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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

        protected async Task<ResponseBaseEntity<TResult>> DoPost<TResult, TRequest>(string uri, TRequest body)
        {
            if (uri.StartsWith("/"))
                uri = uri.Remove(0, 1);

            var response = await httpClient.PostAsJsonAsync(uri, body);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<TResult>();
                    return GetResponseModel("성공", responseContent);
                }
            }
            catch (NotSupportedException) // When content type is not valid
            {
                return GetResponseModel("The content type is not supported.", default(TResult));
            }
            catch (JsonException) // Invalid JSON
            {
                return GetResponseModel("Invalid JSON.", default(TResult));
            }

            throw new Exception();
        }

        //private ResponseBaseEntity GetResponseModel(string message)
        //{
        //    Console.WriteLine(message);

        //    return new ResponseBaseEntity
        //    {
        //        Message = message,
        //    };
        //}

        private ResponseBaseEntity<T> GetResponseModel<T>(string message, T data)
        {
            Console.WriteLine(message);

            return new ResponseBaseEntity<T>
            {
                Message = message,
                Data = data,
            };
        }
    }
}
