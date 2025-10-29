using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.Models
{
    public class CategoriaModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Relacionamentos
        public virtual ICollection<ExercicioModel> Exercicios { get; set; } = new List<ExercicioModel>();

    }
}
