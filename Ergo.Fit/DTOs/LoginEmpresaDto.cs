using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class LoginEmpresaDto
    {
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve ter 14 dígitos.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
