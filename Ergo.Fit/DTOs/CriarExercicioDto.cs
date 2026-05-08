using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class CriarExercicioDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [StringLength(500)]
        public string? VideoUrl { get; set; }

        public int? DuracaoEstimada { get; set; }

        [StringLength(1000)]
        public string? Instrucoes { get; set; }

        [Required(ErrorMessage = "A Categoria é obrigatória.")]
        public int IdCategoria { get; set; }
    }
}
