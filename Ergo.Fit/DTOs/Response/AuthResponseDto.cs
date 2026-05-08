namespace Ergo.Fit.DTOs.Response
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(2);
        public int? EmpresaId { get; set; }
        public int? FuncionarioId { get; set; }
    }
}
