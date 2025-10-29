using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.FuncionarioService
{
    public interface IFuncionarioInterface
    {
        Task<ServiceResponse<List<FuncionarioModel>>> GetFuncionarios();

        Task<ServiceResponse<FuncionarioModel>> CriarFuncionario(CriarFuncionarioDto dto);

        Task<ServiceResponse<FuncionarioModel>> GetFuncionarioById(int id);

        Task<ServiceResponse<List<FuncionarioModel>>> UpdateFuncionario(FuncionarioModel editadoFuncionario);

        Task<ServiceResponse<List<FuncionarioModel>>> DeleteFuncionario(int id);

        Task<ServiceResponse<List<FuncionarioModel>>> InativaFuncionario(int id);

    }
}
