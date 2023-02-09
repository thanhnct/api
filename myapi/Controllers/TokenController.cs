using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myapi.Services;
using NETCore.Encrypt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace myapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly TokenHelper _token;
        public TokenController(ILogger<TokenController> logger, TokenHelper token)
        {
            _logger = logger;
            _token = token;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_token.GenerateToken());
        }
    }
}