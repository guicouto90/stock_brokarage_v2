using Application.Shared;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.LoginContext.Outputs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UseCases.LoginContext
{
    public class AuthenticateUseCase : IRequestHandler<LoginInput, Output<LoginOutput>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticateUseCase> _logger;

        public AuthenticateUseCase(
            ICustomerRepository customerRepository,
            IConfiguration configuration,
            ILogger<AuthenticateUseCase> logger)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Output<LoginOutput>> ExecuteAsync(LoginInput input)
        {
            var output = new Output<LoginOutput>();
            try
            {
                var result = await _customerRepository.GetByCpfAsync(input.CustomerCpf);
                if (result is null || !result.Account.VerifyPassword(input.Password))
                {
                    _logger.LogError($"AuthenticateUseCase, Error: Cpf/password invalid");
                    output.AddErrorMessage("Cpf/password invalid");
                    return output;
                }

                output.AddResult(GenerateJwt(_configuration, result.Id, result.Account.Id, result.Account.AccountNumber));

                return output;

            } catch (Exception ex)
            {
                _logger.LogError($"AuthenticateUseCase, Error: {ex.Message}");
                output.AddErrorMessage($"Error to login for cpf: {input.CustomerCpf}");
                return output;
            }
            
        }

        private static LoginOutput GenerateJwt(IConfiguration configuration, int customerId, int accountId, int accountNumber)
        {
            //declarações do usuário
            var claims = new[]
            {
            new Claim("customerId", $"{customerId}"),
            new Claim("accountId", $"{accountId}"),
            new Claim("accountNumber", $"{accountNumber}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //Id do token
        };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])); //Formato em bytes, cryptografado

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(60);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: configuration["Jwt:Issuer"],
                //audiencia
                audience: configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiracao
                expires: expiration,
                //assinatura digital
                signingCredentials: credentials
                );
            var tokenFinal = new JwtSecurityTokenHandler().WriteToken(token);
            return new LoginOutput(tokenFinal, expiration);
        }
    }
}
