using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            var name = Dns.GetHostName(); // get container id
            var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            var port = HttpContext.Connection.LocalPort;

            return Ok($"Host Name: { Environment.MachineName} \t {name}\t {ip}\t Port: {port}");
        }
    }
}
