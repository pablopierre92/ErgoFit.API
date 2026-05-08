using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.SessaoService
{
    public interface ISessaoInterface
    {
        Task<ServiceResponse<SessaoModel>> IniciarSessao(IniciarSessaoDto dto);
        Task<ServiceResponse<SessaoModel>> FinalizarSessao(int id, FinalizarSessaoDto dto);
        Task<ServiceResponse<List<SessaoModel>>> GetSessoesPorFuncionario(int idFuncionario);
        Task<ServiceResponse<List<SessaoModel>>> GetSessoesPorEmpresa(int idEmpresa);
        Task<ServiceResponse<SessaoModel>> GetSessaoAtiva(int idFuncionario);
    }
}
