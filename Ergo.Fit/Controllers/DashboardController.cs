using Ergo.Fit.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Empresa,UsuarioMaster")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("empresa/{idEmpresa}")]
        public async Task<IActionResult> GetDashboard(int idEmpresa)
        {
            var totalColaboradores = await _context.Funcionarios
                .CountAsync(f => f.IdEmpresa == idEmpresa && f.Ativo);

            var totalSessoes = await _context.Sessoes
                .Where(s => s.Funcionario.IdEmpresa == idEmpresa)
                .CountAsync();

            var sessoesConcluidas = await _context.Sessoes
                .Where(s => s.Funcionario.IdEmpresa == idEmpresa && s.Status == "Concluída")
                .CountAsync();

            var tempoTotalSegundos = await _context.Sessoes
                .Where(s => s.Funcionario.IdEmpresa == idEmpresa && s.DuracaoReal.HasValue)
                .SumAsync(s => s.DuracaoReal ?? 0);

            var exerciciosMaisUsados = await _context.Sessoes
                .Where(s => s.Funcionario.IdEmpresa == idEmpresa)
                .GroupBy(s => new { s.IdExercicio, s.Exercicio.Nome })
                .Select(g => new { Exercicio = g.Key.Nome, Total = g.Count() })
                .OrderByDescending(x => x.Total)
                .Take(5)
                .ToListAsync();

            var sessoesUltimos7Dias = await _context.Sessoes
                .Where(s => s.Funcionario.IdEmpresa == idEmpresa
                    && s.DataHoraInicio >= DateTime.Now.AddDays(-7))
                .GroupBy(s => s.DataHoraInicio.Date)
                .Select(g => new { Data = g.Key, Total = g.Count() })
                .OrderBy(x => x.Data)
                .ToListAsync();

            var ultimasSessoes = await _context.Sessoes
                .Include(s => s.Funcionario)
                .Include(s => s.Exercicio)
                .Where(s => s.Funcionario.IdEmpresa == idEmpresa)
                .OrderByDescending(s => s.DataHoraInicio)
                .Take(10)
                .Select(s => new
                {
                    s.Id,
                    NomeFuncionario = s.Funcionario.Nome + " " + s.Funcionario.Sobrenome,
                    NomeExercicio = s.Exercicio.Nome,
                    s.DataHoraInicio,
                    s.Status,
                    DuracaoSegundos = s.DuracaoReal
                })
                .ToListAsync();

            return Ok(new
            {
                totalColaboradores,
                totalSessoes,
                sessoesConcluidas,
                tempoTotalMinutos = tempoTotalSegundos / 60,
                exerciciosMaisUsados,
                sessoesUltimos7Dias,
                ultimasSessoes
            });
        }
    }
}
