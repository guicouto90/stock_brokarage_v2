using Application.Shared;
using Application.UseCases.CustomerContext.Inputs;
using Application.UseCases.CustomerContext.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IRequestHandler<CustomerCreateInput, Output<object>> _createCustomerUseCase;

        public CustomerController(IRequestHandler<CustomerCreateInput, Output<object>> createCustomerUseCase)
        {
            _createCustomerUseCase = createCustomerUseCase;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateInput input)
        {
            var output = await _createCustomerUseCase.ExecuteAsync(input).ConfigureAwait(false);
            if (!output.IsValid) return BadRequest(output.ErrorMessage);

            return Created("", output.Data);

        }
    }
}
