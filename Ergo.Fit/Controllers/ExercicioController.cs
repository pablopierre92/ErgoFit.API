using Ergo.Fit.DTOs;
using Ergo.Fit.Service.ExercicioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExercicioController : ControllerBase
    {
        private readonly IExercicioInterface _exercicioService;

        public ExercicioController(IExercicioInterface exercicioService)
        {
            _exercicioService = exercicioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercicios()
        {
            var response = await _exercicioService.GetExercicios();
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercicioById(int id)
        {
            var response = await _exercicioService.GetExercicioById(id);
            return response.Sucesso ? Ok(response) : NotFound(response);
        }

        [HttpGet("por-categoria/{idCategoria}")]
        public async Task<IActionResult> GetPorCategoria(int idCategoria)
        {
            var response = await _exercicioService.GetExerciciosPorCategoria(idCategoria);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<IActionResult> CriarExercicio([FromBody] CriarExercicioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _exercicioService.CriarExercicio(dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
