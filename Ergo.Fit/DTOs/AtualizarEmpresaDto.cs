using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class AtualizarEmpresaDto
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime? DataVencimento { get; set; }
    }
}
