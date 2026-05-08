using Ergo.Fit.DTOs;
using Ergo.Fit.Service.SessaoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SessaoController : ControllerBase
    {
        private readonly ISessaoInterface _sessaoService;

        public SessaoController(ISessaoInterface sessaoService)
        {
            _sessaoService = sessaoService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarSessao([FromBody] IniciarSessaoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _sessaoService.IniciarSessao(dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id}/finalizar")]
        public async Task<IActionResult> FinalizarSessao(int id, [FromBody] FinalizarSessaoDto dto)
        {
            var response = await _sessaoService.FinalizarSessao(id, dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpGet("funcionario/{idFuncionario}")]
        public async Task<IActionResult> GetPorFuncionario(int idFuncionario)
        {
            var response = await _sessaoService.GetSessoesPorFuncionario(idFuncionario);
            return Ok(response);
        }

        [HttpGet("funcionario/{idFuncionario}/ativa")]
        public async Task<IActionResult> GetSessaoAtiva(int idFuncionario)
        {
            var response = await _sessaoService.GetSessaoAtiva(idFuncionario);
            return Ok(response);
        }

        [HttpGet("empresa/{idEmpresa}")]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<IActionResult> GetPorEmpresa(int idEmpresa)
        {
            var response = await _sessaoService.GetSessoesPorEmpresa(idEmpresa);
            return Ok(response);
        }
    }
}
