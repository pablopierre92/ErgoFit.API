using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Ergo.Fit.Service.DepartamentoService;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        private readonly IDepartamentoInterface _departamentoInterface;
        public DepartamentoController(IDepartamentoInterface departamentoInterface)
        {
            _departamentoInterface = departamentoInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<DepartamentoModel>>>> GetDepartamentos()
        {
            return Ok(await _departamentoInterface.GetDepartamentos());
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<DepartamentoModel>>> CriarDepartamento([FromBody] CriarDepartamentoDto dto)
        {
            var resultado = await _departamentoInterface.CriarDepartamento(dto);
            return Ok(resultado); // Retorna DepartamentoModel na resposta
        }

    }
}

