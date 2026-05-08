using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class AtualizarFuncionarioDto
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Matricula { get; set; }

        public int? IdDepartamento { get; set; }

        public DateTime? DataAdmissao { get; set; }
    }
}
