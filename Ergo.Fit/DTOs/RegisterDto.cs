using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A senha deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        public string? Password { get; set; }

    }
}
