using Ergo.Fit.DataContext;
using Ergo.Fit.DTOs;
using Ergo.Fit.DTOs.Response;
using Ergo.Fit.Models;
using Ergo.Fit.Service.TokenService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ergo.Fit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Usuario");
            return Ok(new { message = "Usuário criado com sucesso!" });
        }

        // Login para Colaborador (email + senha)
        [HttpPost("login/colaborador")]
        public async Task<IActionResult> LoginColaborador([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Funcionario"))
                return Unauthorized(new { message = "Acesso negado. Use o login de empresa." });

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.ApplicationUserId == user.Id);

            var token = _tokenService.GenerateToken(user, roles);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = user.NomeCompleto,
                Roles = roles.ToList(),
                FuncionarioId = funcionario?.Id
            });
        }

        // Login para Empresa (CNPJ + senha)
        [HttpPost("login/empresa")]
        public async Task<IActionResult> LoginEmpresa([FromBody] LoginEmpresaDto model)
        {
            var cnpjLimpo = new string(model.Cnpj.Where(char.IsDigit).ToArray());

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(e => e.Cnpj == cnpjLimpo && e.Ativo);

            if (empresa == null)
                return Unauthorized(new { message = "CNPJ não encontrado ou empresa inativa" });

            var user = await _userManager.FindByIdAsync(empresa.ApplicationUserId!);

            if (user == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = empresa.Nome,
                Roles = roles.ToList(),
                EmpresaId = empresa.Id
            });
        }

        // Login para UsuarioMaster (email + senha)
        [HttpPost("login/master")]
        public async Task<IActionResult> LoginMaster([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("UsuarioMaster"))
                return Unauthorized(new { message = "Acesso negado. Use o login correto." });

            var token = _tokenService.GenerateToken(user, roles);
            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = user.NomeCompleto,
                Roles = roles.ToList()
            });
        }

        // Mantém /login genérico para retrocompatibilidade
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            return await LoginColaborador(model);
        }
    }
}
