using Ergo.Fit.DTOs;
using Ergo.Fit.Service.CategoriaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaInterface _categoriaService;

        public CategoriaController(ICategoriaInterface categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var response = await _categoriaService.GetCategorias();
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<IActionResult> CriarCategoria([FromBody] CriarCategoriaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _categoriaService.CriarCategoria(dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
