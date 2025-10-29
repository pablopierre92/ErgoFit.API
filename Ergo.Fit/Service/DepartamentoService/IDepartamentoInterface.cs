using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.DepartamentoService
{
    public interface IDepartamentoInterface
    {
        Task<ServiceResponse<List<DepartamentoModel>>> GetDepartamentos();

        Task<ServiceResponse<DepartamentoModel>> CriarDepartamento(CriarDepartamentoDto dto);

    }
}
