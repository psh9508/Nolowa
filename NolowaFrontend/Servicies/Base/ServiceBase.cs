﻿using NolowaFrontend.Models;
using NolowaFrontend.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
            if(httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri("http://localhost:8080/");
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
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

            try
            {
                var response = await httpClient.PostAsJsonAsync(uri, body);

                var debug = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<TResult>();
                    return GetResponseModel(true, "성공", responseContent);
                }
            }
            catch (NotSupportedException) // When content type is not valid
            {
                return GetResponseModel(false, "The content type is not supported.", default(TResult));
            }
            catch (JsonException) // Invalid JSON
            {
                return GetResponseModel(false, "Invalid JSON.", default(TResult));
            }

            return new ResponseBaseEntity<TResult>();
        }

        protected async Task<ResponseBaseEntity<TResult>> DoPost<TResult, TRequest>(string uri, string jsonRowData)
        {
            if (uri.StartsWith("/"))
                uri = uri.Remove(0, 1);

            try
            {
                var content = new StringContent(jsonRowData, Encoding.UTF8, "application/json");

                var result = await httpClient.PostAsync(uri, content);

                var debug = await result.Content.ReadAsStringAsync();

                return new ResponseBaseEntity<TResult>()
                {
                    IsSuccess = result.IsSuccessStatusCode,
                    ResponseData = await result.Content.ReadFromJsonAsync<TResult>(),
                };
            }
            catch(HttpRequestException ex)
            {
                return new ResponseBaseEntity<TResult>()
                {
                    IsSuccess = false,
                    ResponseData = default(TResult),
                    Message = ex.Message,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected async Task<ResponseBaseEntity<TResult>> DoGet<TResult>(string uri)
        {
            if (uri.StartsWith("/"))
                uri = uri.Remove(0, 1);

            try
            {
                var result = await httpClient.GetAsync(uri);

                var debug = await result.Content.ReadAsStringAsync();

                return new ResponseBaseEntity<TResult>()
                {
                    IsSuccess = result.IsSuccessStatusCode,
                    ResponseData = await result.Content.ReadFromJsonAsync<TResult>(),
                };
            }
            catch (HttpRequestException ex)
            {
                return new ResponseBaseEntity<TResult>()
                {
                    IsSuccess = false,
                    ResponseData = default(TResult),
                    Message = ex.Message,
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //private ResponseBaseEntity GetResponseModel(string message)
        //{
        //    Console.WriteLine(message);

        //    return new ResponseBaseEntity
        //    {
        //        Message = message,
        //    };
        //}

        private ResponseBaseEntity<T> GetResponseModel<T>(bool isSuccess, string message, T data)
        {
            Console.WriteLine(message);

            return new ResponseBaseEntity<T>
            {
                IsSuccess = isSuccess,
                Message = message,
                ResponseData = data,
            };
        }
    }
}
