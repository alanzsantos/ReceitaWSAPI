using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReceitaWSAPI.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public BasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetcoreapi\"");
                return;
            }

            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            var configUsername = _configuration["BasicAuth:Username"];
            var configPassword = _configuration["BasicAuth:Password"];

            if (username != configUsername || password != configPassword)
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
