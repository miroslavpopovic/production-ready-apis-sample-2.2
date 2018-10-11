using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BoardGamesApi.Controllers
{
    public class TempController : Controller
    {
        private readonly IConfiguration _configuration;

        public TempController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // NOT FOR PRODUCTION USE!!!
        // you will need a robust auth implementation for production
        // i.e. use ASP.NET Identity with manual token generation or try IdentityServer
        [AllowAnonymous]
        [Route("/get-token")]
        public IActionResult GenerateToken(string name = "aspnetcore-api-demo", bool admin = false)
        {
            var jwt = JwtTokenGenerator
                .Generate(name, admin, _configuration["Tokens:Issuer"], _configuration["Tokens:Key"]);

            return Ok(jwt);
        }
    }
}
