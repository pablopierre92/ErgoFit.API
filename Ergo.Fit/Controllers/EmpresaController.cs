using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Ergo.Fit.Service.EmpresaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaInterface _empresaInterface;

        public EmpresaController(IEmpresaInterface empresaInterface)
        {
            _empresaInterface = empresaInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<EmpresaModel>>>> GetEmpresas()
        {
            return Ok(await _empresaInterface.GetEmpresas());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<EmpresaModel>>> CriarEmpresa([FromBody] CriarEmpresaDto dto)
        {
            var resultado = await _empresaInterface.CriarEmpresa(dto);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<ActionResult<ServiceResponse<EmpresaModel>>> AtualizarEmpresa(int id, [FromBody] AtualizarEmpresaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resultado = await _empresaInterface.AtualizarEmpresa(id, dto);
            return resultado.Sucesso ? Ok(resultado) : BadRequest(resultado);
        }
    }
}
