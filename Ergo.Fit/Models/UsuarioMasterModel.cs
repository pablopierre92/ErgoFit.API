using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergo.Fit.Models
{
    public class UsuarioMasterModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        // Foreign Key
        [StringLength(450)]
        public string? ApplicationUserId { get; set; }

        // Relacionamentos
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<EmpresaModel> EmpresasCriadas { get; set; } = new List<EmpresaModel>();

    }
}
