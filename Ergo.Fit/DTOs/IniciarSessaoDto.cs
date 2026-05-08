using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class IniciarSessaoDto
    {
        [Required]
        public int IdFuncionario { get; set; }

        [Required]
        public int IdExercicio { get; set; }
    }

    public class FinalizarSessaoDto
    {
        [Range(1, 5)]
        public int? Avaliacao { get; set; }

        [StringLength(1000)]
        public string? Observacoes { get; set; }
    }
}
