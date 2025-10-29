using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class CriarEmpresaDto
    {

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve ter 14 dígitos")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; set; } = string.Empty;

        public DateTime? DataVencimento { get; set; }

    }
}
