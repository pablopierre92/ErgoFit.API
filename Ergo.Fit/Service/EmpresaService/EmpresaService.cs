using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Microsoft.AspNetCore.Identity;

namespace Ergo.Fit.Service.EmpresaService
{
    public class EmpresaService : IEmpresaInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmpresaService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<EmpresaModel>> CriarEmpresa(CriarEmpresaDto dto)
        {
            // Cria o ApplicationUser com senha hasheada
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = dto.Nome,
                EmailConfirmed = true,
                Ativo = true
            };

            var result = await _userManager.CreateAsync(user, dto.Senha);

            if (!result.Succeeded)
            {
                return new ServiceResponse<EmpresaModel>
                {
                    Sucesso = false,
                    Mensagem = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            // Adiciona role de Empresa
            await _userManager.AddToRoleAsync(user, "Empresa");

            // Cria a empresa vinculada ao ApplicationUser
            var empresa = new EmpresaModel
            {
                Nome = dto.Nome,
                Cnpj = dto.Cnpj,
                Email = dto.Email,
                ApplicationUserId = user.Id,
                DataVencimento = dto.DataVencimento,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Ativo = true
            };

            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();

            return new ServiceResponse<EmpresaModel> { Dados = empresa };
        }

        public async Task<ServiceResponse<List<EmpresaModel>>> GetEmpresas()
        {
            var response = new ServiceResponse<List<EmpresaModel>>();
            try
            {
                response.Dados = _context.Empresas.ToList();
                if (response.Dados.Count == 0)
                    response.Mensagem = "Nenhum dado encontrado!";
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Sucesso = false;
            }
            return response;
        }

        public async Task<ServiceResponse<EmpresaModel>> AtualizarEmpresa(int id, AtualizarEmpresaDto dto)
        {
            var response = new ServiceResponse<EmpresaModel>();
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Empresa não encontrada.";
                    return response;
                }
                empresa.Nome = dto.Nome;
                empresa.Email = dto.Email;
                empresa.DataVencimento = dto.DataVencimento;
                empresa.DataAtualizacao = DateTime.Now;
                await _context.SaveChangesAsync();
                response.Dados = empresa;
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
