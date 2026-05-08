using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Service.ExercicioService
{
    public class ExercicioService : IExercicioInterface
    {
        private readonly ApplicationDbContext _context;

        public ExercicioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ExercicioModel>>> GetExercicios()
        {
            var response = new ServiceResponse<List<ExercicioModel>>();
            try
            {
                response.Dados = await _context.Exercicios
                    .Include(e => e.Categoria)
                    .Where(e => e.Ativo)
                    .OrderBy(e => e.Nome)
                    .ToListAsync();

                if (response.Dados.Count == 0)
                    response.Mensagem = "Nenhum exercício encontrado.";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<ExercicioModel>>> GetExerciciosPorCategoria(int idCategoria)
        {
            var response = new ServiceResponse<List<ExercicioModel>>();
            try
            {
                response.Dados = await _context.Exercicios
                    .Include(e => e.Categoria)
                    .Where(e => e.Ativo && e.IdCategoria == idCategoria)
                    .OrderBy(e => e.Nome)
                    .ToListAsync();

                if (response.Dados.Count == 0)
                    response.Mensagem = "Nenhum exercício encontrado para esta categoria.";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ExercicioModel>> GetExercicioById(int id)
        {
            var response = new ServiceResponse<ExercicioModel>();
            try
            {
                var exercicio = await _context.Exercicios
                    .Include(e => e.Categoria)
                    .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);

                if (exercicio == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Exercício não encontrado.";
                    return response;
                }
                response.Dados = exercicio;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<ExercicioModel>> CriarExercicio(CriarExercicioDto dto)
        {
            var response = new ServiceResponse<ExercicioModel>();
            try
            {
                var exercicio = new ExercicioModel
                {
                    Nome = dto.Nome,
                    Descricao = dto.Descricao,
                    VideoUrl = dto.VideoUrl,
                    DuracaoEstimada = dto.DuracaoEstimada,
                    Instrucoes = dto.Instrucoes,
                    IdCategoria = dto.IdCategoria,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                };
                _context.Exercicios.Add(exercicio);
                await _context.SaveChangesAsync();
                response.Dados = exercicio;
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
