using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergo.Fit.Models
{
    public class FuncionarioModel
    {
        [Key]
        public int Id { get; set; }

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

        public bool Ativo { get; set; } = true;

        public DateTime? DataAdmissao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime DataDeAlteracao { get; set; } = DateTime.Now;

        // Foreign Keys
        [Required]
        public int IdEmpresa { get; set; }

        public int? IdDepartamento { get; set; }

        // Relacionamentos
        [ForeignKey("IdEmpresa")]
        public virtual EmpresaModel Empresa { get; set; } = null!;

        [ForeignKey("IdDepartamento")]
        public virtual DepartamentoModel? Departamento { get; set; }

        public virtual ICollection<SessaoModel> Sessoes { get; set; } = new List<SessaoModel>();

        // Propriedade calculada para nome completo
        [NotMapped]
        public string NomeCompleto => $"{Nome} {Sobrenome}";
    }
}
