using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.BusinessLayer.Services.AdminService;
using StoreAndDeliver.DataLayer.Models;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace StoreAndDeliver.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly string _connectionString;

        public AdminController(IAdminService adminService, IConfiguration configuration)
        {
            _adminService = adminService;
            _connectionString = configuration["ConnectionStrings:Default"];
        }

        [HttpPost("backupDatabase")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> BackupDatabase()
        {
            await _adminService.BackupDatabase(_connectionString);
            return Ok();
        }

        [HttpGet("getLogs")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetLogs([FromQuery] DateTime date)
        {
            LogsDto logs = await _adminService.GetLogs(date);
            return Ok(logs);
        }
    }
}
