using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergo.Fit.Models
{
    public class DepartamentoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        // Foreign Key
        [Required]
        public int IdEmpresa { get; set; }

        // Relacionamentos
        [ForeignKey("IdEmpresa")]
        public virtual EmpresaModel Empresa { get; set; } = null!;

        public virtual ICollection<FuncionarioModel> Funcionarios { get; set; } = new List<FuncionarioModel>();

    }
}
