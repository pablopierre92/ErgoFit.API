using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Service.CategoriaService
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CategoriaModel>>> GetCategorias()
        {
            var response = new ServiceResponse<List<CategoriaModel>>();
            try
            {
                response.Dados = await _context.Categorias
                    .Where(c => c.Ativo)
                    .OrderBy(c => c.Nome)
                    .ToListAsync();

                if (response.Dados.Count == 0)
                    response.Mensagem = "Nenhuma categoria encontrada.";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CategoriaModel>> CriarCategoria(CriarCategoriaDto dto)
        {
            var response = new ServiceResponse<CategoriaModel>();
            try
            {
                var categoria = new CategoriaModel
                {
                    Nome = dto.Nome,
                    Descricao = dto.Descricao,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                };
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                response.Dados = categoria;
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
