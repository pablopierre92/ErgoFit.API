using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(255)]
        public string Senha { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        // Relacionamentos
        public virtual ICollection<EmpresaModel> EmpresasCriadas { get; set; } = new List<EmpresaModel>();

    }
}
