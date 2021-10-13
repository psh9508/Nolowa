using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models.Base
{
    //public class ResponseBaseEntity
    //{
    //    public int StatusCode { get; set; }
    //    public string Message { get; set; }
    //}

    public class ResponseBaseEntity<T> where T : new()
    {
        public bool IsSuccess { get; set; }
        //public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T ResponseData { get; set; } // 이름이 너무 일반적임
    }
}
