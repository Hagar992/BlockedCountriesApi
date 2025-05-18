using Microsoft.AspNetCore.Mvc;
using BlockedCountriesApi.Models;
using System.Collections.Generic;
using BlockedCountriesApi.Services;
using BlockedCountriesApi.Storage;

namespace BlockedCountriesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly IBlockedAttemptsLogger _logger;

        public LogsController(IBlockedAttemptsLogger logger)
        {
            _logger = logger;
        }

        // GET: api/logs
        [HttpGet]
        public IActionResult GetLogs([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var logs = _logger.GetAll(page, pageSize);
            return Ok(logs);
        }

        // GET: api/logs/count
        [HttpGet("count")]
        public IActionResult GetLogCount()
        {
            var count = AppMemoryStore.BlockedAttempts.Count;
            return Ok(new { TotalBlockedAttempts = count });
        }
    }
}