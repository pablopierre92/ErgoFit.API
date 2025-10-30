using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Ergo.Fit.Service.FuncionarioService;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioInterface _funcionarioInterface;
        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
            _funcionarioInterface = funcionarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> GetFuncionarios()
        {
            return Ok(await _funcionarioInterface.GetFuncionarios());
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> CriarFuncionario([FromBody] CriarFuncionarioDto dto)
        {
            var resultado = await _funcionarioInterface.CriarFuncionario(dto);
            return Ok(resultado); // Retorna FuncionarioModel na resposta
        }

    }
}
