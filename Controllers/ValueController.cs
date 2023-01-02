using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using VueCoreWebapiBackend.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueCoreWebapiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly JwtHelpers _jwtHelpers;

        public ValueController(JwtHelpers jwtHelpers)
        {
            this._jwtHelpers = jwtHelpers;
        }

        [HttpGet("getValues")]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("getValuesByAuth")]
        [Authorize]
        public IEnumerable<string> GetValuesByAuth()
        {
            return new string[] { "valueAuth1", "valueAuth2" };
        }

        // POST api/<ValueController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpGet("getToken/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetToken(string userName)
        {
            //if (username == null || !_identities.TryGetValue(username, out var identity))
            if (userName == null)
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }

            var token = _jwtHelpers.generateToken(userName);
            return Ok(new { token });
        }
    }
}
