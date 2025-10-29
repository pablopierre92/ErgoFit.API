using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.EmpresaService
{
    public class EmpresaService : IEmpresaInterface
    {
        private readonly ApplicationDbContext _context;
        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<EmpresaModel>> CriarEmpresa(CriarEmpresaDto dto)
        {
            var empresa = new EmpresaModel
            {
                Nome = dto.Nome,
                Cnpj = dto.Cnpj,
                Email = dto.Email,
                Senha = dto.Senha,
                DataVencimento = dto.DataVencimento,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Ativo = true
            };

            // Salva no banco usando EmpresaModel
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return new ServiceResponse<EmpresaModel> { Dados = empresa };
        }

        public async Task<ServiceResponse<List<EmpresaModel>>> GetEmpresas()
        {
            ServiceResponse<List<EmpresaModel>> serviceResponse = new ServiceResponse<List<EmpresaModel>>();

            try
            {
                serviceResponse.Dados = _context.Empresas.ToList();

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
