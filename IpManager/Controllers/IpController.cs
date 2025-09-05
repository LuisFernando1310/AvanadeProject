using IpManager.Contract.Request;
using IpManager.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IpManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class IpController : ControllerBase
    {
        private readonly ILogger<IpController> _logger;
        private readonly IIpService _service;

        public IpController(ILogger<IpController> logger, IIpService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetIp([FromQuery] string ip)
        {
            if (!ModelState.IsValid)
                return BadRequest("IP address is required.");

            var response = await _service.GetIpCountryByIpAddress(ip);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateIp()
        {
            await _service.UpdateIps();
            return Ok("IP addresses updated.");
        }

        [HttpPost("report")]
        public async Task<IActionResult> GetReport([FromBody] ReportRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.GetReport(request.CountryCodes);
            return Ok(response);
        }
    }
}