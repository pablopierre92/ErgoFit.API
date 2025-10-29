using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergo.Fit.Models
{
    public class EmpresaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;

        public DateTime? DataVencimento { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; }

        // Foreign Key
        public int? CriadoPorUsuarioMasterId { get; set; }

        // Relacionamentos
        [ForeignKey("CriadoPorUsuarioMasterId")]
        public virtual UsuarioMasterModel? CriadoPorUsuarioMaster { get; set; }

        public virtual ICollection<FuncionarioModel> Funcionarios { get; set; } = new List<FuncionarioModel>();
        public virtual ICollection<DepartamentoModel> Departamentos { get; set; } = new List<DepartamentoModel>();
    }
}
