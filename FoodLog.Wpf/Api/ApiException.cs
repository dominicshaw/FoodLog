using System;
using System.Net;

namespace FoodLog.Wpf.Api
{
    public class ApiException : Exception
    {
        public HttpStatusCode Code { get; }

        public ApiException(HttpStatusCode code) : base($"The application could not access the API (Response: {code}).")
        {
            Code = code;
        }
    }
}