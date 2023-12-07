using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IRequestHandler<LoginInput, Output<LoginOutput>> _loginUseCase;

        public LoginController(IRequestHandler<LoginInput, Output<LoginOutput>> loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            var result = await _loginUseCase.ExecuteAsync(input).ConfigureAwait(false);

            if (!result.IsValid)
                return Unauthorized(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
