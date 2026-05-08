using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Ergo.Fit.Service.FuncionarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioInterface _funcionarioInterface;
        private readonly ApplicationDbContext _context;

        public FuncionarioController(IFuncionarioInterface funcionarioInterface, ApplicationDbContext context)
        {
            _funcionarioInterface = funcionarioInterface;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> GetFuncionarios()
        {
            return Ok(await _funcionarioInterface.GetFuncionarios());
        }

        [HttpGet("por-empresa/{idEmpresa}")]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<IActionResult> GetPorEmpresa(int idEmpresa)
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Departamento)
                .Where(f => f.IdEmpresa == idEmpresa)
                .Select(f => new
                {
                    f.Id,
                    f.Nome,
                    f.Sobrenome,
                    f.Email,
                    f.Cpf,
                    f.Matricula,
                    f.Ativo,
                    f.DataAdmissao,
                    Departamento = f.Departamento != null ? f.Departamento.Nome : null,
                    f.IdDepartamento
                })
                .ToListAsync();

            return Ok(new { Sucesso = true, Dados = funcionarios });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> GetFuncionarioById(int id)
        {
            return Ok(await _funcionarioInterface.GetFuncionarioById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<ActionResult<ServiceResponse<FuncionarioModel>>> CriarFuncionario([FromBody] CriarFuncionarioDto dto)
        {
            var resultado = await _funcionarioInterface.CriarFuncionario(dto);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> UpdateFuncionario(int id, [FromBody] FuncionarioModel funcionario)
        {
            funcionario.Id = id;
            return Ok(await _funcionarioInterface.UpdateFuncionario(funcionario));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> DeleteFuncionario(int id)
        {
            return Ok(await _funcionarioInterface.DeleteFuncionario(id));
        }

        [HttpPatch("{id}/inativar")]
        [Authorize(Roles = "Empresa,UsuarioMaster")]
        public async Task<ActionResult<ServiceResponse<List<FuncionarioModel>>>> InativarFuncionario(int id)
        {
            return Ok(await _funcionarioInterface.InativaFuncionario(id));
        }
    }
}
