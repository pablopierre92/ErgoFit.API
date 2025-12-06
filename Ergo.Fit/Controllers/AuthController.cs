using Ergo.Fit.DTOs;
using Ergo.Fit.DTOs.Response;
using Ergo.Fit.Models;
using Ergo.Fit.Service.TokenService;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ergo.Fit.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

            // Adicionar role padrão
            await _userManager.AddToRoleAsync(user, "Usuario");

            return Ok(new { message = "Usuário criado com sucesso!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var result = await _signInManager.CheckPasswordSignInAsync(
                user, model.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Credenciais inválidas" });

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = user.NomeCompleto,
                Roles = roles.ToList()
            });
        }
    }
}
