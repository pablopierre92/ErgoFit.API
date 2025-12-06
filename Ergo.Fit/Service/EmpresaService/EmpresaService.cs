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
