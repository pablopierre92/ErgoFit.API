using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.EmpresaService
{
    public interface IEmpresaInterface
    {
        Task<ServiceResponse<EmpresaModel>> CriarEmpresa(CriarEmpresaDto dto);
        Task<ServiceResponse<List<EmpresaModel>>> GetEmpresas();
    }
}
