using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Filters
{
    public class LogFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;

        public LogFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger("LogFilter");
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = Dns.GetHostName(); // get container id
            var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            var port = context.HttpContext.Connection.LocalPort;
            var resStr = $"Host Name: { Environment.MachineName} \t {name}\t {ip}\t Port: {port}";
            _logger.LogInformation(resStr);
            await next();
        }
    }
}
