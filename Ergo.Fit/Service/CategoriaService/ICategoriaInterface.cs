using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.CategoriaService
{
    public interface ICategoriaInterface
    {
        Task<ServiceResponse<List<CategoriaModel>>> GetCategorias();
        Task<ServiceResponse<CategoriaModel>> CriarCategoria(CriarCategoriaDto dto);
    }
}
