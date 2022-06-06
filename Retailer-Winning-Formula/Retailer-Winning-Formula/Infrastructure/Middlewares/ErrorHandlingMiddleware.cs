using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using Retailer_Winning_Formula.Models;
using Serilog;
using Serilog.Events;

namespace Retailer_Winning_Formula.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        private static readonly ILogger Log = Serilog.Log.ForContext<ErrorHandlingMiddleware>();

        private static readonly HashSet<string> HeaderWhitelist = new HashSet<string> { "Content-Type", "Content-Length", "User-Agent" };

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var start = Stopwatch.GetTimestamp();
            try
            {
                await _next(httpContext);
                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

                var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;
                log.Write(level, MessageTemplate, httpContext.Request.Method, GetPath(httpContext), statusCode, elapsedMs);
                if (statusCode == 400 || statusCode == 404)
                {
                    SetErrorMsg(statusCode, httpContext);
                }
            }
            // Never caught, because `LogException()` returns false.
            catch (Exception ex) when (LogException(httpContext,
                GetElapsedMilliseconds(start,
                    Stopwatch.GetTimestamp()),
                ex))
            {
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static bool LogException(HttpContext httpContext, double elapsedMs, Exception ex)
        {
            LogForErrorContext(httpContext)
                .Error(ex, MessageTemplate, httpContext.Request.Method, GetPath(httpContext), 500, elapsedMs);

            return true;
        }

        private static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var loggedHeaders = request.Headers
                .Where(h => HeaderWhitelist.Contains(h.Key))
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            var result = Log
                .ForContext("RequestHeaders", loggedHeaders, destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            return result;
        }

        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }

        private static string GetPath(HttpContext httpContext)
        {
            return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget ?? httpContext.Request.Path.ToString();
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected
           
            SetErrorMsg((int)code, context);
            
        }
        private static void SetErrorMsg(int? code, HttpContext httpContext)
        {
            var errorMsg = new ErrorViewModel();
            errorMsg.StatusCode = code;
            if (code == 400)
                errorMsg.Message = "A bad request was received";
            else if (code == 404)
                errorMsg.Message = "Page not found";
            else if (code > 499)
                errorMsg.Message = "Something Went wrong, Please Contact Admin";

            errorMsg.RequestId = httpContext.TraceIdentifier;
            var result = JsonConvert.SerializeObject(errorMsg);
            httpContext.Response.Redirect("/Home/Error");
            httpContext.Session.SetString(Constants.ErrorMessages.ErrorMsg, result);
        }
    }
}