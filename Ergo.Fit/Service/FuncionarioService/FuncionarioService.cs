using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Service.FuncionarioService
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FuncionarioService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<FuncionarioModel>> CriarFuncionario(CriarFuncionarioDto dto)
        {
            // Cria o ApplicationUser com senha hasheada
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = $"{dto.Nome} {dto.Sobrenome}",
                EmailConfirmed = true,
                Ativo = true
            };

            var result = await _userManager.CreateAsync(user, dto.Senha);

            if (!result.Succeeded)
            {
                return new ServiceResponse<FuncionarioModel>
                {
                    Sucesso = false,
                    Mensagem = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            // Adiciona role de Funcionario
            await _userManager.AddToRoleAsync(user, "Funcionario");

            // Cria o funcionário vinculado ao ApplicationUser
            var funcionario = new FuncionarioModel
            {
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Email = dto.Email,
                ApplicationUserId = user.Id,
                Cpf = dto.Cpf,
                Matricula = dto.Matricula,
                DataAdmissao = dto.DataAdmissao,
                IdEmpresa = dto.IdEmpresa,
                IdDepartamento = dto.IdDepartamento,
                DataCriacao = DateTime.Now,
                DataDeAlteracao = DateTime.Now,
                Ativo = true
            };

            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return new ServiceResponse<FuncionarioModel> { Dados = funcionario };
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário removido com sucesso!";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id)
        {
            ServiceResponse<FuncionarioModel> serviceResponse = new ServiceResponse<FuncionarioModel>();

            try
            {
                var funcionario = await _context.Funcionarios
                    .Include(f => f.Empresa)
                    .Include(f => f.Departamento)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                serviceResponse.Dados = funcionario;
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
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

        public async Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);

                if (funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                funcionario.Ativo = false;
                funcionario.DataDeAlteracao = DateTime.Now;

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário inativado com sucesso!";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario)
        {
            ServiceResponse<List<FuncionarioModel>> serviceResponse = new ServiceResponse<List<FuncionarioModel>>();

            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == editadoFuncionario.Id);

                if (funcionario == null)
                {
                    serviceResponse.Mensagem = "Funcionário não encontrado!";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                funcionario.Nome = editadoFuncionario.Nome;
                funcionario.Sobrenome = editadoFuncionario.Sobrenome;
                funcionario.Email = editadoFuncionario.Email;
                funcionario.Cpf = editadoFuncionario.Cpf;
                funcionario.Matricula = editadoFuncionario.Matricula;
                funcionario.DataAdmissao = editadoFuncionario.DataAdmissao;
                funcionario.IdDepartamento = editadoFuncionario.IdDepartamento;
                funcionario.DataDeAlteracao = DateTime.Now;

                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Funcionarios.ToListAsync();
                serviceResponse.Mensagem = "Funcionário atualizado com sucesso!";
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
