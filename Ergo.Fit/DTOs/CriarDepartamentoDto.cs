using System.ComponentModel.DataAnnotations;

namespace Ergo.Fit.DTOs
{
    public class CriarDepartamentoDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }

        [Required]
        public int IdEmpresa { get; set; }

    }
}
