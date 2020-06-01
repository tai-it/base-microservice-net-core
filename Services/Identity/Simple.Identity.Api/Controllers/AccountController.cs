namespace Simple.Identity.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Simple.Identity.Api.Domain.Entities;
    using Simple.Identity.Api.Infrastructure.ActionResults;
    using Simple.Identity.Api.Infrastructure.Filters;
    using Simple.Identity.Api.Models;
    using Simple.Identity.Api.Services;

    [Route("api/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService<ApplicationUser> _identityService;

        public AccountController(IIdentityService<ApplicationUser> identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var responseModel = await _identityService.LoginAsync(model);
            return new CustomActionResult(responseModel);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var responseModel = await _identityService.RegisterAsync(model);
            return new CustomActionResult(responseModel);
        }
    }
}
