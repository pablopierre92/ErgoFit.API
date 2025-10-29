using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ergo.Fit.Models
{
    public class SessaoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataHoraInicio { get; set; }

        public DateTime? DataHoraFim { get; set; }

        /// <summary>
        /// Duração real em segundos (calculada automaticamente)
        /// </summary>
        public int? DuracaoReal { get; set; }

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        /// <summary>
        /// Status da sessão: Iniciada, Concluída, Cancelada, Pausada
        /// </summary>
        [StringLength(20)]
        public string Status { get; set; } = "Iniciada";

        /// <summary>
        /// Avaliação do funcionário sobre o exercício (1-5 estrelas)
        /// </summary>
        [Range(1, 5)]
        public int? Avaliacao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Foreign Keys
        [Required]
        public int IdFuncionario { get; set; }

        [Required]
        public int IdExercicio { get; set; }

        // Relacionamentos
        [ForeignKey("IdFuncionario")]
        public virtual FuncionarioModel Funcionario { get; set; } = null!;

        [ForeignKey("IdExercicio")]
        public virtual ExercicioModel Exercicio { get; set; } = null!;

        // Propriedades calculadas
        [NotMapped]
        public string DuracaoRealFormatada
        {
            get
            {
                if (!DuracaoReal.HasValue) return "Não calculado";

                var timeSpan = TimeSpan.FromSeconds(DuracaoReal.Value);
                if (timeSpan.TotalHours >= 1)
                    return $"{(int)timeSpan.TotalHours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
                else if (timeSpan.TotalMinutes >= 1)
                    return $"{timeSpan.Minutes}m {timeSpan.Seconds}s";
                else
                    return $"{timeSpan.Seconds}s";
            }
        }

        [NotMapped]
        public bool EstaEmAndamento => DataHoraFim == null && Status == "Iniciada";

        [NotMapped]
        public bool EstaConcluida => DataHoraFim != null && Status == "Concluída";

        // Método para calcular duração automaticamente
        public void CalcularDuracao()
        {
            if (DataHoraFim.HasValue)
            {
                var duracao = DataHoraFim.Value - DataHoraInicio;
                DuracaoReal = (int)duracao.TotalSeconds;
            }
        }

    }
}
