using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace CompanyName.ProjectName.Api.Middlewares
{
    /// <summary>
    ///     请求日志中间件
    /// </summary>
    public class RequestLogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        /// <inheritdoc />
        public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 读取请求内容
        /// </summary>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return bodyAsText;
        }

        /// <summary>
        /// 读取输出内容
        /// </summary>
        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        /// <summary>
        ///     调用方法
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            _logger.LogInformation($"[request]" +
                $" {GetIsometricString(context.Connection.RemoteIpAddress.ToString(), 25)}" +
                $" {GetIsometricString(stopwatch.ElapsedMilliseconds.ToString(), 6, rightPlaceholder: false)} ms" +
                $" {GetIsometricString(context.Request.Method, 7, rightPlaceholder: false)}" +
                $" {GetIsometricString(context.Response.StatusCode.ToString(), 3, rightPlaceholder: false)}" +
                $" {UriHelper.GetDisplayUrl(context.Request)}");
        }

        /// <summary> 
        /// 获取等长文本(中文2字节，英文1字节)
        /// </summary>
        private string GetIsometricString(string str, int maxLength, bool rightPlaceholder = true)
        {
            int GetContentLength(string content)
            {
                return Regex.Replace(content, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length;
            }
            var temp = str;
            var contentLength = GetContentLength(temp);
            if (contentLength <= maxLength)
            {
                var placeholder = new string(' ', maxLength - contentLength);
                if (rightPlaceholder)
                {
                    return str + placeholder;
                }
                else
                {
                    return placeholder + str;
                }
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (GetContentLength(temp) <= maxLength - 3)
                {
                    return temp + "...";
                }
            }
            return "...";
        }
    }

    /// <summary>
    ///     请求处理中间件拓展
    /// </summary>
    public static class RequestLogMiddlewareExtensions
    {
        /// <summary>
        /// before calling .UseStaticFiles method.
        /// </summary>
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
