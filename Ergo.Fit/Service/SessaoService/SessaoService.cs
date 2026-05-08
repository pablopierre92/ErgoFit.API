using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Service.SessaoService
{
    public class SessaoService : ISessaoInterface
    {
        private readonly ApplicationDbContext _context;

        public SessaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<SessaoModel>> IniciarSessao(IniciarSessaoDto dto)
        {
            var response = new ServiceResponse<SessaoModel>();
            try
            {
                var sessao = new SessaoModel
                {
                    IdFuncionario = dto.IdFuncionario,
                    IdExercicio = dto.IdExercicio,
                    DataHoraInicio = DateTime.Now,
                    Status = "Iniciada",
                    DataCriacao = DateTime.Now
                };
                _context.Sessoes.Add(sessao);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Sessoes
                    .Include(s => s.Exercicio).ThenInclude(e => e.Categoria)
                    .FirstAsync(s => s.Id == sessao.Id);
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<SessaoModel>> FinalizarSessao(int id, FinalizarSessaoDto dto)
        {
            var response = new ServiceResponse<SessaoModel>();
            try
            {
                var sessao = await _context.Sessoes.FindAsync(id);
                if (sessao == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Sessão não encontrada.";
                    return response;
                }

                sessao.DataHoraFim = DateTime.Now;
                sessao.Status = "Concluída";
                sessao.Avaliacao = dto.Avaliacao;
                sessao.Observacoes = dto.Observacoes;
                sessao.CalcularDuracao();

                _context.Sessoes.Update(sessao);
                await _context.SaveChangesAsync();
                response.Dados = sessao;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<SessaoModel>>> GetSessoesPorFuncionario(int idFuncionario)
        {
            var response = new ServiceResponse<List<SessaoModel>>();
            try
            {
                response.Dados = await _context.Sessoes
                    .Include(s => s.Exercicio).ThenInclude(e => e.Categoria)
                    .Where(s => s.IdFuncionario == idFuncionario)
                    .OrderByDescending(s => s.DataHoraInicio)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<SessaoModel>>> GetSessoesPorEmpresa(int idEmpresa)
        {
            var response = new ServiceResponse<List<SessaoModel>>();
            try
            {
                response.Dados = await _context.Sessoes
                    .Include(s => s.Funcionario)
                    .Include(s => s.Exercicio).ThenInclude(e => e.Categoria)
                    .Where(s => s.Funcionario.IdEmpresa == idEmpresa)
                    .OrderByDescending(s => s.DataHoraInicio)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<SessaoModel>> GetSessaoAtiva(int idFuncionario)
        {
            var response = new ServiceResponse<SessaoModel>();
            try
            {
                var sessao = await _context.Sessoes
                    .Include(s => s.Exercicio).ThenInclude(e => e.Categoria)
                    .FirstOrDefaultAsync(s => s.IdFuncionario == idFuncionario && s.Status == "Iniciada");

                response.Dados = sessao;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }
    }
}
