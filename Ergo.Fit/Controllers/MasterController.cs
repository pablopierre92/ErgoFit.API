using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.Models;
using Ergo.Fit.Service.CategoriaService;
using Ergo.Fit.Service.EmpresaService;
using Ergo.Fit.Service.ExercicioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Controllers
{
    [ApiController]
    [Route("api/master")]
    [Authorize(Roles = "UsuarioMaster")]
    public class MasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriaInterface _categoriaService;
        private readonly IExercicioInterface _exercicioService;
        private readonly IEmpresaInterface _empresaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MasterController(
            ApplicationDbContext context,
            ICategoriaInterface categoriaService,
            IExercicioInterface exercicioService,
            IEmpresaInterface empresaService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _categoriaService = categoriaService;
            _exercicioService = exercicioService;
            _empresaService = empresaService;
            _userManager = userManager;
        }

        // ===== CATEGORIAS =====

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var cats = await _context.Categorias.OrderBy(c => c.Nome).ToListAsync();
            return Ok(new { Sucesso = true, Dados = cats });
        }

        [HttpPost("categorias")]
        public async Task<IActionResult> CriarCategoria([FromBody] CriarCategoriaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _categoriaService.CriarCategoria(dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpPut("categorias/{id}")]
        public async Task<IActionResult> EditarCategoria(int id, [FromBody] CriarCategoriaDto dto)
        {
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return NotFound(new { message = "Categoria não encontrada." });
            cat.Nome = dto.Nome;
            cat.Descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return Ok(new { Sucesso = true, Dados = cat });
        }

        [HttpDelete("categorias/{id}")]
        public async Task<IActionResult> DeletarCategoria(int id)
        {
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return NotFound(new { message = "Categoria não encontrada." });
            cat.Ativo = false;
            await _context.SaveChangesAsync();
            return Ok(new { Sucesso = true, message = "Categoria inativada." });
        }

        // ===== EXERCÍCIOS =====

        [HttpGet("exercicios")]
        public async Task<IActionResult> GetExercicios()
        {
            var exs = await _context.Exercicios
                .Include(e => e.Categoria)
                .OrderBy(e => e.Categoria.Nome).ThenBy(e => e.Nome)
                .ToListAsync();
            return Ok(new { Sucesso = true, Dados = exs });
        }

        [HttpPost("exercicios")]
        public async Task<IActionResult> CriarExercicio([FromBody] CriarExercicioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _exercicioService.CriarExercicio(dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpPut("exercicios/{id}")]
        public async Task<IActionResult> EditarExercicio(int id, [FromBody] CriarExercicioDto dto)
        {
            var ex = await _context.Exercicios.FindAsync(id);
            if (ex == null) return NotFound(new { message = "Exercício não encontrado." });
            ex.Nome = dto.Nome;
            ex.Descricao = dto.Descricao;
            ex.VideoUrl = dto.VideoUrl;
            ex.DuracaoEstimada = dto.DuracaoEstimada;
            ex.Instrucoes = dto.Instrucoes;
            ex.IdCategoria = dto.IdCategoria;
            await _context.SaveChangesAsync();
            return Ok(new { Sucesso = true, Dados = ex });
        }

        [HttpDelete("exercicios/{id}")]
        public async Task<IActionResult> DeletarExercicio(int id)
        {
            var ex = await _context.Exercicios.FindAsync(id);
            if (ex == null) return NotFound(new { message = "Exercício não encontrado." });
            ex.Ativo = false;
            await _context.SaveChangesAsync();
            return Ok(new { Sucesso = true, message = "Exercício inativado." });
        }

        // ===== EMPRESAS =====

        [HttpGet("empresas")]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _context.Empresas
                .OrderBy(e => e.Nome)
                .ToListAsync();
            return Ok(new { Sucesso = true, Dados = empresas });
        }

        [HttpPost("empresas")]
        public async Task<IActionResult> CriarEmpresa([FromBody] CriarEmpresaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _empresaService.CriarEmpresa(dto);
            return response.Sucesso ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("empresas/{id}")]
        public async Task<IActionResult> InativarEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null) return NotFound(new { message = "Empresa não encontrada." });
            empresa.Ativo = false;
            empresa.DataAtualizacao = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(new { Sucesso = true, message = "Empresa inativada." });
        }

        // ===== USUÁRIOS MASTER =====

        [HttpGet("usuarios")]
        public async Task<IActionResult> GetMasters()
        {
            var masters = await _context.UsuariosMaster
                .Where(m => m.Ativo)
                .OrderBy(m => m.Nome)
                .ToListAsync();
            return Ok(new { Sucesso = true, Dados = masters });
        }

        [HttpPost("usuarios")]
        public async Task<IActionResult> CriarMaster([FromBody] CriarUsuarioMasterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = dto.Nome,
                EmailConfirmed = true,
                Ativo = true
            };

            var result = await _userManager.CreateAsync(user, dto.Senha);
            if (!result.Succeeded)
                return BadRequest(new { message = string.Join(", ", result.Errors.Select(e => e.Description)) });

            await _userManager.AddToRoleAsync(user, "UsuarioMaster");

            _context.UsuariosMaster.Add(new UsuarioMasterModel
            {
                Nome = dto.Nome,
                Email = dto.Email,
                ApplicationUserId = user.Id,
                Ativo = true
            });
            await _context.SaveChangesAsync();

            return Ok(new { Sucesso = true, message = "Usuário master criado com sucesso!" });
        }
    }
}
