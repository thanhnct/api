using Microsoft.AspNetCore.Mvc;
using myapi.DTOs;
using myapi.Services;

namespace myapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlePayController : ControllerBase
    {
        private readonly ILogger<AlePayController> _logger;
        private readonly IConfiguration _configuration;
        private readonly TokenService _token;
        private readonly AlepayService _repo;

        public AlePayController(ILogger<AlePayController> logger, IConfiguration configuration, TokenService token, AlepayService repo)
        {
            _configuration = configuration;
            _logger = logger;
            _token = token;
            _repo = repo;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost(Name = "RequestPayment")]
        public async Task<IActionResult> Post([FromBody] AlePayRequestPaymentDto model)
        {
            if (!_token.CheckValidToken())
            {
                return Unauthorized();
            }

            return Ok(await _repo.Checkout(model));
        }
    }
}