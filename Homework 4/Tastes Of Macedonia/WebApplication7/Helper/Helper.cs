using System;
using System.Net.Http;

namespace WebApplication7.Helper
{
    public class ResApi
    {
        public static HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44379/");
            return client;
        }
    }
}