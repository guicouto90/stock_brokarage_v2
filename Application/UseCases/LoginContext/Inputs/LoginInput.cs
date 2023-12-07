using System.ComponentModel.DataAnnotations;

namespace StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Cpf is required")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "CPF must have 11 numeric characters")]
        public string CustomerCpf { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Password doesnt match")]
        public string ConfirmPassword { get; set; }
    }
}
