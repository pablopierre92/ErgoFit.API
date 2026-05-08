using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class CriarCategoriaDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }
    }
}
