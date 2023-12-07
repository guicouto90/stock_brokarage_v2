using System.ComponentModel.DataAnnotations;
namespace Application.UseCases.CustomerContext.Inputs
{
    public class CustomerCreateInput
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "CPF must have 11 numeric characters")]
        public string Cpf { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must have at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Password doesnt match")]
        public string ConfirmPassword { get; set; }
    }
}
