using Ergo.Fit.DTOs;
using Ergo.Fit.Models;

namespace Ergo.Fit.Service.ExercicioService
{
    public interface IExercicioInterface
    {
        Task<ServiceResponse<List<ExercicioModel>>> GetExercicios();
        Task<ServiceResponse<List<ExercicioModel>>> GetExerciciosPorCategoria(int idCategoria);
        Task<ServiceResponse<ExercicioModel>> GetExercicioById(int id);
        Task<ServiceResponse<ExercicioModel>> CriarExercicio(CriarExercicioDto dto);
    }
}
