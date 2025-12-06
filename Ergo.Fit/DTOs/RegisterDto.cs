using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class RegisterDto
    {

        [Required(ErrorMessage = "Nome completo é obrigatório")]
        [StringLength(200, MinimumLength = 3)]
        public string? NomeCompleto { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A senha deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
