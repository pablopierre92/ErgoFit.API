using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergo.Fit.Models
{
    public class ExercicioModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [StringLength(500)]
        public string? VideoUrl { get; set; }

        /// <summary>
        /// Duração estimada em segundos
        /// </summary>
        public int? DuracaoEstimada { get; set; }

        [StringLength(1000)]
        public string? Instrucoes { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Foreign Key
        [Required]
        public int IdCategoria { get; set; }

        // Relacionamentos
        [ForeignKey("IdCategoria")]
        public virtual CategoriaModel Categoria { get; set; } = null!;

        public virtual ICollection<SessaoModel> Sessoes { get; set; } = new List<SessaoModel>();

        // Propriedade calculada para duração em formato legível
        [NotMapped]
        public string DuracaoFormatada
        {
            get
            {
                if (!DuracaoEstimada.HasValue) return "Não informado";

                var timeSpan = TimeSpan.FromSeconds(DuracaoEstimada.Value);
                if (timeSpan.TotalHours >= 1)
                    return $"{(int)timeSpan.TotalHours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
                else if (timeSpan.TotalMinutes >= 1)
                    return $"{timeSpan.Minutes}m {timeSpan.Seconds}s";
                else
                    return $"{timeSpan.Seconds}s";
            }
        }

    }
}
