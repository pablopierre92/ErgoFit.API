using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class CriarFuncionarioDto
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

        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Matricula { get; set; }

        public DateTime? DataAdmissao { get; set; }

        [Required]
        public int IdEmpresa { get; set; }

        public int? IdDepartamento { get; set; }

    }
}
