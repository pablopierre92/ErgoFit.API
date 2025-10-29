using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.DepartamentoService 
{
    public class DepartamentoService : IDepartamentoInterface
    {
        private readonly ApplicationDbContext _context;
        public DepartamentoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<DepartamentoModel>> CriarDepartamento(CriarDepartamentoDto dto)
        {
            // Converte DTO para Model
            var departamento = new DepartamentoModel
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                IdEmpresa = dto.IdEmpresa,

            };

            // Salva no banco usando DepartamentoModel
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();

            return new ServiceResponse<DepartamentoModel> { Dados = departamento };
        }

        public async Task<ServiceResponse<List<DepartamentoModel>>> GetDepartamentos()
        {
            ServiceResponse<List<DepartamentoModel>> serviceResponse = new ServiceResponse<List<DepartamentoModel>>();

            try
            {
                serviceResponse.Dados = _context.Departamentos.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }



            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

            }

            return serviceResponse;
        }

    }
}
