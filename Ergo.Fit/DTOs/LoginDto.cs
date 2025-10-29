using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do Email é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
