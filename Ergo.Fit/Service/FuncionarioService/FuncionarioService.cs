using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Service.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly ApplicationDbContext _context;
        public FuncionarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<FuncionarioModel>> CriarFuncionario(CriarFuncionarioDto dto)
        {
            // Converte DTO para Model
            var funcionario = new FuncionarioModel
            {
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Email = dto.Email,
                Senha = dto.Senha,
                Cpf = dto.Cpf,
                Matricula = dto.Matricula,
                DataAdmissao = dto.DataAdmissao,
                IdEmpresa = dto.IdEmpresa,
                IdDepartamento = dto.IdDepartamento,
                // Propriedades automáticas
                DataCriacao = DateTime.Now,
                DataDeAlteracao = DateTime.Now,
                Ativo = true
            };

            // Salva no banco usando FuncionarioModel
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return new ServiceResponse<FuncionarioModel> { Dados = funcionario };
        }

        public Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios()
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                // serviceResponse.Dados = _context.Funcionarios.ToList();

                serviceResponse.Dados = await _context.Funcionarios
                    .Include(e => e.Empresa)  // ← Carrega a empresa
                    .Include(e => e.Departamento)   // ← Carrega os departamentos
                    .ToListAsync();

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

        public Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            throw new NotImplementedException();
        }

    }
}
